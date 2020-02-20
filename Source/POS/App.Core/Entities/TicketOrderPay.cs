namespace App.Core.Entities
{
    public partial class Ticketorderpay : BaseEntity
    {
        public double Subtotal { get; set; }
        public double Tax { get; set; }
        public int PurchaseOrderPayId { get; set; }
        public virtual Purchaseorderpay PurchaseOrderPay { get; set; }
    }
}
