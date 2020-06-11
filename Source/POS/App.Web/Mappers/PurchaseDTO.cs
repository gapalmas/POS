using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Mappers
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public bool Delivery { get; set; }
        public bool Confirm { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateUpdate { get; set; }
        public int Count { get { return this.Orderitemssales.Count; } }
        public virtual ICollection<Orderitemssales> Orderitemssales { get; set; }
        public virtual ICollection<Ticketorder> Ticketorder { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get { return this.Orderitemssales == null ? 0 : this.Orderitemssales.Sum(i => i.Value); } }
    }
}
