using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.Mappers;
using System.IO;
using System;
using App.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace App.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IMapper Mapper;
        public IOperations<Product> OperationsProduct;
        public IOperations<Inventory> OperationsInventory;

        public IOperations<Inventory> OperationsInv { get; }

        public ProductController(IMapper mapper, IOperations<Product> operations, IOperations<Inventory> operationsInv)
        {
            Mapper = mapper;
            OperationsProduct = operations;
            OperationsInventory = operationsInv;
        }

        public async Task<IActionResult> Index()
        {
            var products = await OperationsProduct.FindAllAsync(c => c.Status == true);
            var model = Mapper.Map<IEnumerable<ProductDTO>>(products);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST: Products/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\products", file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Products/{file}";
                }

                var product = ToProduct(view, path);
                await OperationsProduct.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private Product ToProduct(ProductViewModel view, string path)
        {
            return new Product
            {
                ImagePath = path,
                Description = view.Description,
                PartNumber = view.PartNumber,
                Date = DateTime.Now,
                DateUpdate = DateTime.Now,
                Price = view.Price,
                Status = true,
                ClasificationId = 1,
                CategoryId = 1,
                InventoryId = InventoryId()



            };
        }

        private int InventoryId()
        {
           var invetory = OperationsInventory.Create(
                new Inventory() { Stock = 0, StockMin = 0, StockMax = 0, Location = "", UnitId = 1, Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true, Equal = 0, WarehouseId = 1 
                });
            return invetory.Id;
        }
    }
}