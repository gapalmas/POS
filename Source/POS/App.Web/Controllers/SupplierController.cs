using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Supplier> OperationsSup;
        public SupplierController(IMapper Mapper, IOperations<Supplier> OperationsSup)
        {
            this.Mapper = Mapper;
            this.OperationsSup = OperationsSup;
        }

        // GET: Customer
        public async Task<IActionResult> Index(/*int? pageNumber*/)
        {
            //int pageSize = 3;
            return View(Mapper.Map<IList<SupplierDTO>>(await OperationsSup.FindAllAsync(p => p.Status == true)));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierDTO view)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    Address = view.Address,
                    BussinessName = view.BussinessName,
                    CommercialName = view.CommercialName,
                    Cp = view.Cp,
                    DayCredit = view.DayCredit,
                    Rfc = view.Rfc,
                    Date = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    Status = true

                };
                await OperationsSup.CreateAsync(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await OperationsSup.FindAsync(p => p.Id == id.Value);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(Mapper.Map<SupplierDTO>(supplier));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SupplierDTO view)
        {
            if (view == null)
            {
                return NotFound();
            }
            var supplier = await OperationsSup.FindAsync(i => i.Id == view.Id);
            if (supplier != null)
            {
                try
                {
                    supplier.BussinessName = view.BussinessName;
                    supplier.CommercialName = view.CommercialName;
                    supplier.DayCredit = view.DayCredit;
                    supplier.Cp = view.Cp;
                    supplier.Address = view.Address;
                    supplier.Rfc = view.Rfc;
                    supplier.DateUpdate = DateTime.Now;
                    await OperationsSup.UpdateAsync(supplier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OperationsSup.ExistsAsync(p => p.Id == view.Id))
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

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await OperationsSup.GetAsync(id.Value);

            var model = Mapper.Map<SupplierDTO>(supplier);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await OperationsSup.GetAsync(id);
            supplier.Status = false;
            await OperationsSup.UpdateAsync(supplier);
            return RedirectToAction(nameof(Index));
        }
    }
}