using App.Web.Features.Alerts;
using App.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(AlertService alertService)
        {
            _alertService = alertService;
        }

        public AlertService _alertService { get; }

        public IActionResult Index()
        {
            _alertService.Success("This is the alert", true);
            return View();
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
