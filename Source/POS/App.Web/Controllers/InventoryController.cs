using App.Core.Entities;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using App.Web.Mappers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using App.Web.Extensions.Alerts;

namespace App.Web.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Product> OperationsPro;
        private readonly IOperations<Inventory> OperationsInv;
        private readonly IOperations<Inventoryio> OperationsInvIo;
        private readonly IOperations<Customer> OperationsCus;
        private readonly IOperations<Purchaseorder> OperationPur;
        private readonly IOperations<Orderitemssales> OperationIte;


        public InventoryController(
            IMapper Mapper, 
            IOperations<Product> OperationsPro, 
            IOperations<Inventory> OperationsInv, 
            IOperations<Inventoryio> OperationsInvIo, 
            IOperations<Customer> OperationsCus, 
            IOperations<Purchaseorder> OperationPur,
            IOperations<Orderitemssales> OperationIte)
        {
            this.Mapper = Mapper;
            this.OperationsPro = OperationsPro;
            this.OperationsInv = OperationsInv;
            this.OperationsInvIo = OperationsInvIo;
            this.OperationsCus = OperationsCus;
            this.OperationPur = OperationPur;
            this.OperationIte = OperationIte;
        }

        // GET: Inventory
        public async Task<IActionResult> Index()
        {
            return View(Mapper.Map<IList<InventoryDTO>>(await OperationsPro.FindAllIncludeAsync(p => p.Status == true, p => p.Inventory)));
            //int pageSize = 3;
            //return View(PaginatedList<InventoryDTO>.Create(Mapper.Map<IList<InventoryDTO>>(await OperationsPro.FindAllIncludeAsync(p => p.Status == true, p => p.Inventory)).AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: Inventory/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = await OperationsPro.FindIncludeAsync(p => p.InventoryId == id.Value, p => p.Inventory);
            //var products = await OperationsInv.FindAsync(p => p.Id == id.Value);
            if (products == null)
            {
                return NotFound();
            }
            var model = Mapper.Map<AddInventoryDTO>(products);

            return View(model);
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddInventoryDTO view)
        {
            try
            {
                var Inventory = await OperationsInv.GetAsync(view.Id);
                Inventory.Stock += view.Stock;
                Inventory.DateUpdate = DateTime.Now;
                await OperationsInv.UpdateAsync(Inventory);
                var inventoryIo = new Inventoryio 
                { 
                    Quantity = view.Stock, 
                    Date = DateTime.Now, 
                    DateUpdate = DateTime.Now, 
                    Status = true, 
                    InventoryId = view.Id,
                    Price = view.Price
                };
                await OperationsInvIo.CreateAsync(inventoryIo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await OperationsPro.FindIncludeAsync(p => p.InventoryId == id.Value, p => p.Inventory);
            if (product == null)
            {
                return NotFound();
            }
            return View(Mapper.Map<InventoryDTO>(product));
        }
               
        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InventoryDTO view)
        {
            if (view == null)
            {
                return NotFound();
            }
            var inventory = await OperationsInv.FindAsync(i => i.Id == view.InventoryId);
            if (inventory != null)
            {
                try
                {
                    inventory.StockMin = view.Inventory.StockMin;
                    inventory.StockMax = view.Inventory.StockMax;
                    inventory.DateUpdate = DateTime.Now;
                    await OperationsInv.UpdateAsync(inventory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OperationsInv.ExistsAsync(p => p.Id == view.InventoryId))
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

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = await OperationsPro.FindAsync(p => p.InventoryId == id.Value);
            if (products == null)
            {
                return NotFound();
            }
            var Inventory = await OperationsInv.GetAsync(id.Value);
            if (Inventory.Stock <= 0)
            {
                return RedirectToAction(nameof(Index)).WithWarning("Insufficient inventory!", "You need to add more inventory."); ;
            }


                var model = Mapper.Map<RemoveInventoryDTO>(products);

            
            /* Lista para crear productos*/
            var customer = Mapper.Map<IEnumerable<CustomerDTO>>(await OperationsCus.FindAllAsync(c => c.Status == true));

            if (customer == null)
            {
                return NotFound();
            }

            List<CustomerDTO> Customers = new List<CustomerDTO>(customer);
            ViewBag.CustomerList = Customers;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(RemoveInventoryDTO view)
        {
            try
            {
                var Inventory = await OperationsInv.GetAsync(view.InventoryId);
                if (view.Stock > Inventory.Stock)
                {
                    return RedirectToAction(nameof(Index)).WithWarning("Insufficient inventory!", "You need to add more inventory.");
                }
                else
                {
                    Inventory.Stock -= view.Stock;
                    Inventory.DateUpdate = DateTime.Now;
                    await OperationsInv.UpdateAsync(Inventory);
                    var inventoryIo = new Inventoryio
                    {
                        Quantity = view.Stock,
                        Date = DateTime.Now,
                        DateUpdate = DateTime.Now,
                        Status = false,
                        InventoryId = view.InventoryId,
                        Price = view.Price
                    };
                    await OperationsInvIo.CreateAsync(inventoryIo);
                    var po = await OperationPur.CreateAsync(new Purchaseorder { Status = true, CustomerId = view.CustomerId, Date = DateTime.Now, DateUpdate = DateTime.Now });
                    var itempo = await OperationIte.CreateAsync(new Orderitemssales { Status = true, Quantity = view.Stock, ProductId = view.Id, PurchaseOrderId = po.Id, Price = view.Price, Date = DateTime.Now, DateUpdate = DateTime.Now });
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}