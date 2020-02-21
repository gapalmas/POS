﻿using System.Collections.Generic;

namespace App.Core.Entities
{
    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            Purchaseorder = new HashSet<Purchaseorder>();
        }

        public string CommercialName { get; set; }
        public string BussinessName { get; set; }
        public string Rfc { get; set; }
        public decimal Cp { get; set; }
        public string Address { get; set; }
        public int DayCredit { get; set; }
        public virtual ICollection<Purchaseorder> Purchaseorder { get; set; }
    }
}