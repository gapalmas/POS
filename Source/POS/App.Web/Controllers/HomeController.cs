using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.Extensions.Alerts;
using App.Web.Features.Alerts;
using App.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Purchaseorder> OperationsPur;
        private readonly IOperations<Product> OperationsPro;
        private readonly IOperations<Customer> OperationsCus;
        public HomeController(IMapper Mapper, IOperations<Purchaseorder> OperationsPur, IOperations<Product> OperationsPro, IOperations<Customer> OperationsCus)
        {
            this.Mapper = Mapper;
            this.OperationsPur = OperationsPur;
            this.OperationsPro = OperationsPro;
            this.OperationsCus = OperationsCus;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await OperationsPur.CountAsyncCondition(o => o.Status == true);
            var deliveries = await OperationsPur.CountAsyncCondition(d => d.Confirm == true);
            var customers = await OperationsCus.CountAsyncCondition(c=> c.Status == true);
            var products = await OperationsPro.CountAsyncCondition(p => p.Status == true);
            var model = new DashboardViewModel { 
                Orders = orders,
                Deliveries = deliveries, 
                Customers = customers,
                Products = products
            
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}
