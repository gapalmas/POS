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

namespace App.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Purchaseorder> OperationsPur;
        private readonly IOperations<Customer> OperationsCus;
        private readonly IOperations<Product> OperationsPro;

        public PurchaseController(IMapper mapper, IOperations<Purchaseorder> operationsPur, IOperations<Customer> operationsCus, IOperations<Product> operationsPro)
        {
            Mapper = mapper;
            OperationsPur = operationsPur;
            OperationsCus = operationsCus;
            OperationsPro = operationsPro;
        }

        // GET: Purchase
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 3;
            return View(PaginatedList<CustomerDTO>.Create(Mapper.Map<IList<CustomerDTO>>(await OperationsCus.FindAllAsync(c => c.Status == true)).AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: Purchase/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Purchase/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = await OperationsPro.FindAsync(p => p.Id == id.Value);
            if (products == null)
            {
                return NotFound();
            }
            //var model = Mapper.Map<AddInventoryDTO>(products);






            /* Lista para crear productos*/
            var product = Mapper.Map<IEnumerable<ProductDTO>>(await OperationsPro.FindAllAsync(c => c.Status == true));
            if (product == null)
            {
                return NotFound();
            }

            List<ProductDTO> model = new List<ProductDTO>(product);
            ViewBag.ProductList = model;
            return View();
        }

        // POST: Purchase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO view)
        {
            var customer = await OperationsCus.FindAsync(p => p.Id == view.Id);

            if (ModelState.IsValid)
            {
                var purchase = new Purchaseorder
                {
                    CustomerId = view.Id,
                    Date = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    Status = true

                };
                await OperationsPur.CreateAsync(purchase);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: Purchase/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Purchase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Purchase/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Purchase/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}