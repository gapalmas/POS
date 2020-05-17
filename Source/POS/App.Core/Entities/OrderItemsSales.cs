using System.ComponentModel.DataAnnotations;

namespace App.Core.Entities
{
    public partial class Orderitemssales : BaseEntity
    {
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public decimal? Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Purchaseorder PurchaseOrder { get; set; }
    }
}
