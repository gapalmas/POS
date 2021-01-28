using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using AutoMapper;
using App.Web.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Web.Extensions.Alerts;
using System.Diagnostics.Contracts;
using App.Infrastructure.Migrations;
using App.Web.DTOs;

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
        private readonly IOperations<Inventory> OperationsInv;
        private readonly IOperations<Inventoryio> OperationsInvio;

        public PurchaseController(IMapper mapper, IOperations<Purchaseorder> operationsPur, IOperations<Customer> operationsCus, IOperations<Product> operationsPro, IOperations<Orderitemssales> operationsIte, IOperations<Inventory> operationsInv, IOperations<Inventoryio> operationsInvio)
        {
            Mapper = mapper;
            OperationsPur = operationsPur;
            OperationsCus = operationsCus;
            OperationsPro = operationsPro;
            OperationsIte = operationsIte;
            OperationsInv = operationsInv;
            OperationsInvio = operationsInvio;
        }
        /*Muestra las ordenes de compras abiertas*/
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

        /*Accion sin asignar*/
        // GET: Purchase/Details/5
        public IActionResult Details()
        {
            return View();
        }

        /*Muestra una lista de clientes para proceder a crear una orden de compra*/
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

        #region 'Code dummy'
        //// POST: Purchase/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ProductDTO view)
        //{
        //    var customer = await OperationsCus.FindAsync(p => p.Id == view.Id);

        //    if (ModelState.IsValid)
        //    {
        //        var purchase = new Purchaseorder
        //        {
        //            CustomerId = view.Id,
        //            Date = DateTime.Now,
        //            DateUpdate = DateTime.Now,
        //            Status = true,
        //            Delivery =false,
        //            Confirm = false

        //        };
        //        await OperationsPur.CreateAsync(purchase);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(view);
        //}
        ///*Accion sin asignar*/
        //// GET: Purchase/Edit/5
        //public IActionResult Edit()
        //{
        //    return View();
        //}
        #endregion

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
            var client = await OperationsCus.FindAsync(c => c.Id == (int)id);
            TempData["PoId"] = purchase.Id;
            TempData["Percent"] = client.Percent.ToString();
            return this.RedirectToAction("AddProduct");
        }

        /*Muestra los productos agregados de la orden de compra*/
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
            var query = await OperationsIte.GetAllIncludeAsync(c => c.PurchaseOrderId == id.Value && c.Status == true, c => c.Product, c => c.PurchaseOrder.Customer);
            foreach (var item in query)
            {
                ViewBag.Percent = item.PurchaseOrder.Customer.Percent;
            }
            
            var items = Mapper.Map<IEnumerable<PurchaseAddProductDTO>>(query);
            

            return View(items);
        }

        public async Task<IActionResult> AddItem(int? Id)
        {
            var query = await OperationsPur.GetAllIncludeAsync(p => p.Id == Id.Value, p => p.Customer);
            double percent = 0;
            foreach (var item in query)
            {
                percent = item.Customer.Percent;
            }

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
                Percent = percent
                /*Price = 1*/
            };
            return View(model);
        }

        /*
         * ToDO: Agregar opcion para sumar elementos borrados cuando se hayan seleccionado de nuevo
         */
        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var inventario = await OperationsPro.FindIncludeAsync(p => p.Id == model.ProductId, p=> p.Inventory);
                if (model.Quantity > inventario.Inventory.Stock)
                {
                    TempData["PoId"] = model.POId;
                    return this.RedirectToAction("AddProduct").WithWarning("Insufficient inventory!", "You need to add more inventory.");
                }

                var item = new Orderitemssales { 
                    Quantity = model.Quantity, 
                    PurchaseOrderId = model.POId,
                    Date = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    ProductId = model.ProductId,
                    Status = true,
                    Price = inventario.Price * (decimal)(1 + (model.Percent / 100 ))}; /*Cambiar el precio base por el precio de % de customer*/
                var Item = await OperationsIte.FindAsync(i => i.PurchaseOrderId == model.POId && i.ProductId == model.ProductId);

                if (Item == null)
                {
                    Item = await OperationsIte.CreateAsync(item);
                }
                //if (Item.Status == false)
                //{
                //    Item.Quantity = model.Quantity;
                //    Item.Status = true;
                //    Item.DateUpdate= DateTime.Now;
                //    await OperationsIte.UpdateAsync(Item);
                //}
                else 
                {
                    Item.Quantity += model.Quantity;
                    await OperationsIte.UpdateAsync(Item);
                }
                inventario.Inventory.Stock = inventario.Inventory.Stock - model.Quantity;
                inventario.Inventory.DateUpdate = DateTime.Now;
                await OperationsInv.UpdateAsync(inventario.Inventory);
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

            var orderItem = await OperationsIte.FindIncludeAsync(i=> i.Id == id.Value, i=> i.Product.Inventory);
            if (orderItem == null)
            {
                return NotFound();
            }

            if( 1 > orderItem.Product.Inventory.Stock)
            {
                TempData["PoId"] = orderItem.PurchaseOrderId;
                return this.RedirectToAction("AddProduct").WithWarning("Insufficient inventory!", "You need to add more inventory.");
            }
            orderItem.Quantity++;
            orderItem.DateUpdate = DateTime.Now;
            
            if (orderItem.Quantity > 0)
            {
                await OperationsIte.UpdateAsync(orderItem);
            }
            orderItem.Product.Inventory.Stock--;
            orderItem.Product.Inventory.DateUpdate = DateTime.Now;
            await OperationsInv.UpdateAsync(orderItem.Product.Inventory);
            TempData["PoId"] = orderItem.PurchaseOrderId;
            return this.RedirectToAction("AddProduct");
        }
        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orderItem = await OperationsIte.FindIncludeAsync(i => i.Id == id.Value, i => i.Product.Inventory);
            if (orderItem == null)
            {
                return NotFound();
            }
            orderItem.Quantity--;
            orderItem.DateUpdate = DateTime.Now;
            
            if (orderItem.Quantity > 0)
            {
                await OperationsIte.UpdateAsync(orderItem);
                orderItem.Product.Inventory.Stock++;
                orderItem.Product.Inventory.DateUpdate = DateTime.Now;
                await OperationsInv.UpdateAsync(orderItem.Product.Inventory);
            }

            TempData["PoId"] = orderItem.PurchaseOrderId;
            return this.RedirectToAction("AddProduct");
        }

        #region 'Code Dummy'
        /*Codigo sin asigmar*/
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
        #endregion

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orderItem = await OperationsIte.FindIncludeAsync(i => i.Id == id.Value, i => i.Product.Inventory);
            orderItem.Status = false;
            orderItem.DateUpdate = DateTime.Now;
            orderItem.Product.Inventory.Stock = orderItem.Product.Inventory.Stock + orderItem.Quantity;
            OperationsIte.Delete(orderItem);
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
            if (response.Confirm)
            {
                await OperationsPur.UpdateAsync(response);
                return this.RedirectToAction("Index");
            }
            TempData["PoId"] = response.Id;
            return this.RedirectToAction("AddProduct");
        }

        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await OperationsPur.FindAsync(p => p.Id == id.Value);
            response.Confirm = true;
            response.DateUpdate = DateTime.Now;
            if (response.Confirm)
            {
                await OperationsPur.UpdateAsync(response);
            }
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await OperationsPur.FindAsync(p => p.Id == id.Value);
            response.Status = false;
            response.DateUpdate = DateTime.Now;
            if (!response.Status)
            {
                await OperationsPur.UpdateAsync(response);
            }
            return this.RedirectToAction("Index");
        }
    }
}