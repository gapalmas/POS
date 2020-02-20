using System.Collections.Generic;

namespace App.Core.Entities
{
    public partial class Purchaseorderpay : BaseEntity
    {
        public Purchaseorderpay()
        {
            Orderitemspays = new HashSet<Orderitemspays>();
            Ticketorderpay = new HashSet<Ticketorderpay>();
        }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Orderitemspays> Orderitemspays { get; set; }
        public virtual ICollection<Ticketorderpay> Ticketorderpay { get; set; }
    }
}
