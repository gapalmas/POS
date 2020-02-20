using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Core.UseCases;
using App.Infrastructure.Data;
using App.Web.Helpers;
using App.Web.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Unity;

namespace App.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IUserHelper userHelper;
        static ManageOperations<Product> ManageOperationsProduct;

        public ProductController(IMapper mapper, IUserHelper userHelper)
        {
            Mapper = mapper;
            this.userHelper = userHelper;
            InitializeContainer();
        }



        public IActionResult Index()
        {
            //var products = ManageOperationsProduct.FindAll(p => p.Status == true);
            return View(Mapper.Map<ProductDTO>(ManageOperationsProduct.FindAll(p => p.Status == true)));
        }

        static void InitializeContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IOperations<Product>, ManageOperations<Product>>();
            container.RegisterType<IRepository<Product>, GenericRepository<Product>>();
            ManageOperationsProduct = container.Resolve<ManageOperations<Product>>();
        }
    }
}