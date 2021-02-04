using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Entities
{
    public partial class Orderitemssales : BaseEntity
    {
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public double Percent { get { return 1 + (this.PurchaseOrder.Customer.Percent / 100); } }
        public virtual Product Product { get; set; }
        public virtual Purchaseorder PurchaseOrder { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get { return this.Price * (decimal)this.Quantity; } }
    }
}
