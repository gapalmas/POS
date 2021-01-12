using App.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace App.Web.DTOs
{
    public class PurchaseAddProductDTO
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }
        public double Percent { get { return 1 + (this.PurchaseOrder.Customer.Percent / 100); } }
        public virtual Product Product { get; set; }
        public virtual Purchaseorder PurchaseOrder { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Value { get { return this.Price * (decimal)this.Quantity; } }
    }
}
