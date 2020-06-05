using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Mappers
{
    public class PurchaseAddProductDTO
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Purchaseorder PurchaseOrder { get; set; }

        public decimal Value { get { return this.Price * (decimal)this.Quantity; } }
    }
}
