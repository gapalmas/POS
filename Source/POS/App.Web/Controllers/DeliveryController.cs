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
        private readonly IOperations<Product> OperationsPro;
        private readonly IOperations<Inventoryio> OperationsIo;

        public DeliveryController(IMapper mapper, IOperations<Purchaseorder> operationsPur, IOperations<Product> operationsPro, IOperations<Inventoryio> operationsIo)
        {
            Mapper = mapper;
            OperationsPur = operationsPur;
            OperationsPro = operationsPro;
            OperationsIo = operationsIo;
        }
        public async Task<IActionResult> Index()
        {
            return View(Mapper.Map<IEnumerable<PurchaseDTO>>(await OperationsPur.GetAllIncludeAsync(c => c.Status == true && c.Confirm == true && c.Delivery == false, c => c.Orderitemssales, c => c.Customer)));
        }

        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await OperationsPur.FindIncludeAsync(p => p.Id == id.Value, p => p.Orderitemssales);

            foreach (var item in response.Orderitemssales)
            {
                var Inv = await OperationsPro.FindIncludeAsync(i=> i.Id == item.ProductId, i => i.Inventory);
                await OperationsIo.CreateAsync(
                    new Inventoryio() { Date = DateTime.Now, DateUpdate = DateTime.Now, InventoryId = Inv.InventoryId, Price = item.Price, Quantity = item.Quantity, Status = false }
                    );
            }

            response.Delivery = true;
            response.DateUpdate = DateTime.Now;
            if (response.Confirm)
            {
                await OperationsPur.UpdateAsync(response);
                return this.RedirectToAction("Index");
            }
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await OperationsPur.FindIncludeAsync(p => p.Id == id.Value, p => p.Orderitemssales);

            foreach (var item in response.Orderitemssales)
            {
                var Inv = await OperationsPro.FindIncludeAsync(i => i.Id == item.ProductId, i => i.Inventory);
                Inv.Inventory.Stock += item.Quantity;
            }
            response.Status = false;
            response.Delivery = false;
            response.DateUpdate = DateTime.Now;
            if (response.Confirm)
            {
                await OperationsPur.UpdateAsync(response);
                return this.RedirectToAction("Index");
            }
            return this.RedirectToAction("Index");
        }
    }
}
