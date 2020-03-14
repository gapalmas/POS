﻿using App.Core.Entities;
using App.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using App.Web.Mappers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace App.Web.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Product> OperationsPro;
        private readonly IOperations<Inventory> OperationsInv;

        public InventoryController(IMapper Mapper, IOperations<Product> OperationsPro, IOperations<Inventory> OperationsInv)
        {
            this.Mapper = Mapper;
            this.OperationsPro = OperationsPro;
            this.OperationsInv = OperationsInv;
        }

        // GET: Inventory
        public async Task<ActionResult> Index()
        {
            return View(Mapper.Map<IEnumerable<InventoryDTO>>(await OperationsPro.FindAllIncludeAsync(p => p.Status == true, p => p.Inventory)));
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                await OperationsInv.UpdateAsync(Inventory);
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
        public ActionResult Edit(int id, IFormCollection collection)
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
    }
}