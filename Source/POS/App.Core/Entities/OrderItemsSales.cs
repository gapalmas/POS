using System.ComponentModel.DataAnnotations;

namespace App.Core.Entities
{
    public partial class Orderitemssales : BaseEntity
    {
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }
        public double Percent { get { return 1 + (this.PurchaseOrder.Customer.Percent / 100); } }
        public virtual Product Product { get; set; }
        public virtual Purchaseorder PurchaseOrder { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get { return this.Price * (decimal)this.Quantity; } }
    }
}
