using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace App.Core.Entities
{
    public partial class Purchaseorder : BaseEntity
    {
        public Purchaseorder()
        {
            Orderitemssales = new HashSet<Orderitemssales>();
            Ticketorder = new HashSet<Ticketorder>();
        }
        public bool Delivery { get; set; }
        public bool Confirm { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Orderitemssales> Orderitemssales { get; set; }
        public virtual ICollection<Ticketorder> Ticketorder { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get { return this.Orderitemssales == null ? 0 : this.Orderitemssales.Sum(i => i.Value); } }
    }
}
