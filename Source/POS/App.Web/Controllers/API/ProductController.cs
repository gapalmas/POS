using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Infrastructure.Data;
using App.Web.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        public IOperations<Product> OperationsProduct;
        public ProductController(DataContext context, IOperations<Product> operations)
        {
            _context = context;
            OperationsProduct = operations;
        }



        // GET: api/Products
        [HttpGet]
        /*[Authorize]*/ /*Para autenticar el ingreso*/
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Product.Include(x => x.Inventory).ToListAsync();
        }
    }
}