using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IOperations<Customer> OperationsCus;
        public CustomerController(IMapper Mapper, IOperations<Customer> OperationsCus)
        {
            this.Mapper = Mapper;
            this.OperationsCus = OperationsCus;
        }

        // GET: Customer
        public async Task<ActionResult> Index()
        {
            return View(Mapper.Map<IEnumerable<CustomerDTO>>(await OperationsCus.FindAllAsync(p => p.Status == true)));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerDTO view)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer {  
                    Address = view.Address, 
                    BussinessName = view.BussinessName, 
                    CommercialName = view.CommercialName, 
                    Cp = view.Cp, 
                    DayCredit = view.DayCredit, 
                    Rfc = view.Rfc, 
                    Date = DateTime.Now, 
                    DateUpdate = DateTime.Now, 
                    Status = true

                };
                await OperationsCus.CreateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}