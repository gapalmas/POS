using System.Collections.Generic;

namespace App.Core.Entities
{
    public partial class Purchaseorder : BaseEntity
    {
        public Purchaseorder()
        {
            Orderitemssales = new HashSet<Orderitemssales>();
            Ticketorder = new HashSet<Ticketorder>();
        }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Orderitemssales> Orderitemssales { get; set; }
        public virtual ICollection<Ticketorder> Ticketorder { get; set; }
    }
}
