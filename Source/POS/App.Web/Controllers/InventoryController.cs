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

namespace App.Web.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Product> OperationsPro;
        private readonly IOperations<Inventory> OperationsInv;
        private readonly IOperations<Inventoryio> OperationsInvIo;

        public InventoryController(IMapper Mapper, IOperations<Product> OperationsPro, IOperations<Inventory> OperationsInv, IOperations<Inventoryio> OperationsInvIo)
        {
            this.Mapper = Mapper;
            this.OperationsPro = OperationsPro;
            this.OperationsInv = OperationsInv;
            this.OperationsInvIo = OperationsInvIo;
        }

        // GET: Inventory
        public async Task<ActionResult> Index(int? pageNumber)
        {
            int pageSize = 3;
            return View(PaginatedList<InventoryDTO>.Create(Mapper.Map<IList<InventoryDTO>>(await OperationsPro.FindAllIncludeAsync(p => p.Status == true, p => p.Inventory)).AsQueryable(), pageNumber ?? 1, pageSize));
        }

        // GET: Inventory/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = await OperationsInv.FindAsync(p => p.Id == id.Value);
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
        public async Task<ActionResult> Create(AddInventoryDTO view)
        {
            try
            {
                var Inventory = await OperationsInv.GetAsync(view.Id);
                Inventory.Stock = (Inventory.Stock + view.Stock);
                Inventory.DateUpdate = DateTime.Now;
                await OperationsInv.UpdateAsync(Inventory);
                var inventoryIo = new Inventoryio 
                { 
                    Quantity = view.Stock, 
                    Date = DateTime.Now, 
                    DateUpdate = DateTime.Now, 
                    Status = true, 
                    InventoryId = view.Id
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
        public async Task<ActionResult> Edit(int? id)
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
        public async Task<ActionResult> Edit(InventoryDTO view)
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
    }
}