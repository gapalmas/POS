using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Inventory> OperationsInv;

        public ReportController(IMapper Mapper, IOperations<Inventory> OperationsInv)
        {
            this.Mapper = Mapper;
            this.OperationsInv = OperationsInv;
        }

        public async Task<IActionResult> MissingInventory()
        {
            return View(Mapper.Map<IList<InventoryReportDTO>>(await OperationsInv.FindAllIncludeAsync(p => p.Status == true && p.Stock <= p.StockMin, p => p.Product)));
        }
    }
}
