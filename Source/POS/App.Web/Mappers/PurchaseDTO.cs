using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Mappers
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Orderitemssales> Orderitemssales { get; set; }
        public virtual ICollection<Ticketorder> Ticketorder { get; set; }
    }
}
