using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Purchaseorder> OperationsPur;

        public DeliveryController(IMapper mapper, IOperations<Purchaseorder> operationsPur)
        {
            Mapper = mapper;
            OperationsPur = operationsPur;
        }
        public async Task<IActionResult> Index()
        {
            return View(Mapper.Map<IEnumerable<PurchaseDTO>>(await OperationsPur.GetAllIncludeAsync(c => c.Status == true && c.Confirm == true && c.Delivery == false, c => c.Orderitemssales, c => c.Customer)));
        }
    }
}
