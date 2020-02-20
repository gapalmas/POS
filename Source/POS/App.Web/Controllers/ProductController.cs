using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.Mappers;
using System.IO;
using System;

namespace App.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper Mapper;
        public IOperations<Product> OperationsProduct;
        public ProductController(IMapper mapper, IOperations<Product> operations)
        {
            Mapper = mapper;
            OperationsProduct = operations;
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

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,Status,Stock,StockMin,StockMax")] Product view)
        //{
            //if (ModelState.IsValid)
            //{
            //    var guid = Guid.NewGuid().ToString();
            //    var path = string.Empty;

            //    if (view.ImagePath != null && view.ImagePath.Length > 0)
            //    {
            //        //var guid = Guid.NewGuid().ToString();
            //        var file = $"{guid}.jpg";

            //        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\products", file);

            //        using (var stream = new FileStream(path, FileMode.Create))
            //        {
            //            await view.ImagePath.CopyToAsync(stream);
            //        }

            //        path = $"~/images/Products/{file}";
            //    }

            //    var product = ToProduct(view, path);
            //    //product.User = await userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            //    await productRepository.CreateAsync(product);
            //    return RedirectToAction(nameof(Index));
            //}

            //return View(view);

            //return View(product);
        //}
    }
}