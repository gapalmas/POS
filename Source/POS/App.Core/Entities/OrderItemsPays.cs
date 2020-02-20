namespace App.Core.Entities
{
    public partial class Orderitemspays : BaseEntity
    {
        public double Quantity { get; set; }
        public int PurchaseOrderPayId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Purchaseorderpay PurchaseOrderPay { get; set; }
    }
}
