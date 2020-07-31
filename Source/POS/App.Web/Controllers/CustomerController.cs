using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Web.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index(/*int? pageNumber*/)
        {
            //int pageSize = 3;
            return View(Mapper.Map<IList<CustomerDTO>>(await OperationsCus.FindAllAsync(p => p.Status == true)));
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerDTO view)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer {  
                    Address = view.Address, 
                    BussinessName = view.BussinessName, 
                    CommercialName = view.CommercialName, 
                    Cp = view.Cp, 
                    DayCredit = view.DayCredit, 
                    Percent = view.Percent,
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await OperationsCus.FindAsync(p => p.Id == id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(Mapper.Map<CustomerDTO>(customer));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerDTO view)
        {
            if (view == null)
            {
                return NotFound();
            }
            var customer = await OperationsCus.FindAsync(i => i.Id == view.Id);
            if (customer != null)
            {
                try
                {
                    customer.BussinessName = view.BussinessName;
                    customer.CommercialName = view.CommercialName;
                    customer.DayCredit = view.DayCredit;
                    customer.Percent = view.Percent;
                    customer.Cp = view.Cp;
                    customer.Address = view.Address;
                    customer.Rfc = view.Rfc;
                    customer.DateUpdate = DateTime.Now;
                    await OperationsCus.UpdateAsync(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OperationsCus.ExistsAsync(p => p.Id == view.Id))
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

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await OperationsCus.GetAsync(id.Value);

            var model = Mapper.Map<CustomerDTO>(customer);

            if (customer == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await OperationsCus.GetAsync(id);
            customer.Status = false;
            await OperationsCus.UpdateAsync(customer);
            return RedirectToAction(nameof(Index));
        }
    }
}