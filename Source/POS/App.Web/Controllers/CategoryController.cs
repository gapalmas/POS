using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Category> OperationsCat;

        public CategoryController(IMapper Mapper, IOperations<Category> OperationsCat)
        {
            this.Mapper = Mapper;
            this.OperationsCat = OperationsCat;
        }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            return View(Mapper.Map<IList<CategoryDTO>>(await OperationsCat.FindAllAsync(c => c.Status == true)));
            //return View();
        }

        // GET: CategoryController/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    return View();
        //}

        // GET: CategoryController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO view)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Description = view.Description.ToUpper(),
                    Date = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    Status = true
                };
                await OperationsCat.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await OperationsCat.FindAsync(p => p.Id == id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(Mapper.Map<CategoryDTO>(category));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDTO view)
        {
            if (view == null)
            {
                return NotFound();
            }
            var category = await OperationsCat.FindAsync(i => i.Id == view.Id);
            if (category != null)
            {
                try
                {
                    category.Description = view.Description;
                    category.DateUpdate = DateTime.Now;
                    await OperationsCat.UpdateAsync(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OperationsCat.ExistsAsync(p => p.Id == view.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await OperationsCat.GetAsync(id.Value);

            var model = Mapper.Map<CategoryDTO>(category);

            if (category == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await OperationsCat.GetAsync(id);
            category.Status = false;
            await OperationsCat.UpdateAsync(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
