using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Product> OperationsPro;
        public ProductController(IMapper Mapper, IOperations<Product> OperationsPro)
        {
            this.Mapper = Mapper;
            this.OperationsPro = OperationsPro;
        }

        // GET: api/Products
        [HttpGet]
        /*[Authorize]*/ /*Para autenticar el ingreso*/
        public async Task<ActionResult<IEnumerable<ApiProductDTO>>> GetProducts()
        {
            return Mapper.Map<IEnumerable<ApiProductDTO>>(await OperationsPro.FindAllAsync(p => p.Status == true)).ToList();
        }
    }
}