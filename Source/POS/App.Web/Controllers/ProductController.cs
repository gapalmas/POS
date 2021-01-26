using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using System.IO;
using System;
using App.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using App.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Web.DTOs;

namespace App.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Product> OperationsPro;
        private readonly IOperations<Inventory> OperationsInv;
        private readonly IOperations<Category> OperationsCat;

        public ProductController(IMapper Mapper, IOperations<Product> OperationsPro, IOperations<Inventory> OperationsInv, IOperations<Category> OperationsCat)
        {
            this.Mapper = Mapper;
            this.OperationsPro = OperationsPro;
            this.OperationsInv = OperationsInv;
            this.OperationsCat = OperationsCat;
        }

        public async Task<IActionResult> Index()
        {
            //return View(Mapper.Map<IList<ProductDTO>>(await OperationsPro.FindAllAsync(c => c.Status == true)));
            return View(Mapper.Map<IList<ProductDTO>>(await OperationsPro.GetAllIncludeAsync(p => p.Status == true, c =>  c.Category )));
            //int pageSize = 3;
            //return View(PaginatedList<ProductDTO>.Create(Mapper.Map<IList<ProductDTO>>(await OperationsPro.FindAllAsync(c => c.Status == true)).AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Create()
        {
            List<Category> items = new List<Category>(await OperationsCat.FindAllAsync(p => p.Status == true));
            var list = items.Select(p => new SelectListItem
            {
                Text = p.Description,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a category...)",
                Value = "0"
            });

            var model = new ProductViewModel
            {
                Categories = list
                /*Price = 1*/
            };
            return View(model);

            //return View();
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

                    path = $"~/img/products/{file}";
                }
                var product = ToProduct(view, path);
                await OperationsPro.CreateAsync(product);
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
                CategoryId = view.CategoryId,
                InventoryId = InventoryId()
            };
        }

        private int InventoryId()
        {
           var invetory = OperationsInv.Create(
                new Inventory() { Stock = 0, StockMin = 0, StockMax = 0, Location = "", UnitId = 1, Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true, Equal = 0, WarehouseId = 1 
                });
            return invetory.Id;
        }


        [Authorize(Roles = "Admin")]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await OperationsPro.GetAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            var view = ToProductViewModel(product);
            return View(view);
        }

        private ProductViewModel ToProductViewModel(Product product)
        {
            List<Category> items = new List<Category>(OperationsCat.FindAll(p => p.Status == true));
            
            var list = items.Select(p => new SelectListItem
            {
                Text = p.Description,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a category...)",
                Value = "0"
            });

            return new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                InventoryId = product.InventoryId,
                ImagePath = product.ImagePath,
                PartNumber = product.PartNumber,
                Price = product.Price,
                Categories = list
            };
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await OperationsPro.GetAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // ToDO : Evaluate ImageFile for show path or not.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel view)
        {
            if (view == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid | view.ImageFile is null)
            {
                try
                {
                    var path = view.ImagePath;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\products", file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/img/products/{file}";
                    }
                    var product = OperationsPro.Find(p => p.Id == view.Id);

                    product.Description = view.Description;
                    product.Price = view.Price;
                    product.ImagePath = path;
                    product.PartNumber = view.PartNumber;
                    product.DateUpdate = DateTime.Now;
                    product.CategoryId = view.CategoryId;

                    //var products = ToProduct(view, path);
                    await OperationsPro.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OperationsPro.ExistsAsync(p => p.Id == view.Id))
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

        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await OperationsPro.GetAsync(id.Value);

            var model = Mapper.Map<ProductDTO>(product);

            if (product == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await OperationsPro.GetAsync(id);
            product.Status = false;
            await OperationsPro.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductNotFound()
        {
            return this.View();
        }
    }
}