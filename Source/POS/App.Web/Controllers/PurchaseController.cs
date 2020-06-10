using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.Mappers;
using AutoMapper;
using App.Web.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Web.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Purchaseorder> OperationsPur;
        private readonly IOperations<Orderitemssales> OperationsIte;
        private readonly IOperations<Customer> OperationsCus;
        private readonly IOperations<Product> OperationsPro;

        public PurchaseController(IMapper mapper, IOperations<Purchaseorder> operationsPur, IOperations<Customer> operationsCus, IOperations<Product> operationsPro, IOperations<Orderitemssales> operationsIte)
        {
            Mapper = mapper;
            OperationsPur = operationsPur;
            OperationsCus = operationsCus;
            OperationsPro = operationsPro;
            OperationsIte = operationsIte;
        }

        public async Task<IActionResult> Index()
        {
            //var customers = Mapper.Map<IEnumerable<CustomerListDTO>>(await OperationsCus.FindAllAsync(c => c.Status == true));

            //if (customers == null)
            //{
            //    return NotFound();
            //}
            //List<CustomerListDTO> model = new List<CustomerListDTO>(customers);
            //ViewBag.CustomerList = model;

            //int pageSize = 3;
            return View(Mapper.Map<IEnumerable<PurchaseDTO>>(await OperationsPur.GetAllIncludeAsync(c => c.Status == true && c.Confirm == false, c => c.Orderitemssales, c=> c.Customer )));
            //return View(PaginatedList<PurchaseDTO>.Create(Mapper.Map<IList<PurchaseDTO>>(await OperationsPur.FindAllIncludeAsync(c => c.Status == true, c => c.Customer)).AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: Purchase/Details/5
        public IActionResult Details()
        {
            return View();
        }

        // GET: Purchase/Create
        public async Task<IActionResult> Create(/*int? id*/)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            var customer = Mapper.Map<IEnumerable<CustomerListDTO>>(await OperationsCus.FindAllAsync(c => c.Status == true));
            return View(customer);
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
                    Status = true,
                    Delivery =false,
                    Confirm = false

                };
                await OperationsPur.CreateAsync(purchase);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: Purchase/Edit/5
        public IActionResult Edit()
        {
            return View();
        }

        public async Task<IActionResult> AddOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await OperationsPur.CreateAsync(new Purchaseorder
            {
                Date = DateTime.Now,
                DateUpdate = DateTime.Now,
                Status = true,
                CustomerId = (int)id
            });
            TempData["PoId"] = purchase.Id;
            return this.RedirectToAction("AddProduct");
        }

        public async Task<IActionResult> AddProduct(int? id)
        {
            if(TempData["PoId"] != null)
            {
                id = TempData["PoId"] as int?;
            }

            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Id = id.Value;
            var items = Mapper.Map<IEnumerable<PurchaseAddProductDTO>>(await OperationsIte.FindAllIncludeAsync(c => c.PurchaseOrderId == id.Value && c.Status==true , c => c.Product));
            return View(items.OrderBy(i => i.Product));
        }

        public async Task<IActionResult> AddItem(int? Id)
        {
            List<Product> items = new List<Product>(await OperationsPro.FindAllAsync(p => p.Status == true));
            var list = items.Select(p => new SelectListItem
            {
                Text = p.Description,
                Value = p.Id.ToString()                
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a product...)",
                Value = "0"                
            });

            var model = new AddItemViewModel
            {
                Quantity = 1,
                Products = list,
                POId = Id.Value,
                Price = 1
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var item = new Orderitemssales { 
                    Quantity = model.Quantity, 
                    PurchaseOrderId = model.POId,
                    Date = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    ProductId = model.ProductId,
                    Status = true,
                    Price = model.Price };
                var Item = await OperationsIte.FindAsync(i => i.PurchaseOrderId == model.POId && i.ProductId == model.ProductId);

                if (Item == null)
                {
                    await OperationsIte.CreateAsync(item);
                }
                else 
                {
                    Item.Quantity += model.Quantity;
                    await OperationsIte.UpdateAsync(Item);
                }
                TempData["PoId"] = model.POId;
                return this.RedirectToAction("AddProduct");
            }
            return this.View(model);
        }
        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await OperationsIte.FindAsync(i=> i.Id == id.Value);
            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.Quantity++;
            if (orderItem.Quantity > 0)
            {
                await OperationsIte.UpdateAsync(orderItem);
            }

            TempData["PoId"] = orderItem.PurchaseOrderId;
            return this.RedirectToAction("AddProduct");
        }
        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await OperationsIte.FindAsync(i => i.Id == id.Value);
            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.Quantity--;
            if (orderItem.Quantity > 0)
            {
                await OperationsIte.UpdateAsync(orderItem);
            }

            TempData["PoId"] = orderItem.PurchaseOrderId;
            return this.RedirectToAction("AddProduct");
        }


        // POST: Purchase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orderItem = await OperationsIte.FindAsync(i=> i.Id == id.Value);
            orderItem.Status = false;

            await OperationsIte.UpdateAsync(orderItem);

            TempData["PoId"] = orderItem.PurchaseOrderId;
            return this.RedirectToAction("AddProduct");
        }

        public async Task<IActionResult> ConfirmOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await OperationsPur.FindAsync(p=> p.Id == id.Value);
            response.Confirm = true;
            response.DateUpdate = DateTime.Now;
            if (response.Confirm)
            {
                await OperationsPur.UpdateAsync(response);
                return this.RedirectToAction("Index");
            }
            TempData["PoId"] = response.Id;
            return this.RedirectToAction("AddProduct");
        }
    }
}