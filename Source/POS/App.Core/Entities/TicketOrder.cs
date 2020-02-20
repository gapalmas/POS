namespace App.Core.Entities
{
    public partial class Ticketorder : BaseEntity
    {
        public double Subtotal { get; set; }
        public double Tax { get; set; }
        public int PurchaseOrderId { get; set; }
        public virtual Purchaseorder PurchaseOrder { get; set; }
    }
}
