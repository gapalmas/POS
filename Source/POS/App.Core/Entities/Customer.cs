using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Entities
{
    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            Purchaseorder = new HashSet<Purchaseorder>();
        }
        [Display(Name = "Commercial Name")]
        public string CommercialName { get; set; }
        public string BussinessName { get; set; }
        public string Rfc { get; set; }
        [RegularExpression(@"^\d+\.\d{0,0}$")]
        [Column(TypeName = "decimal(5,0)")]
        public decimal Cp { get; set; }
        public string Address { get; set; }
        public int DayCredit { get; set; }
        public double Percent { get; set; }
        public virtual ICollection<Purchaseorder> Purchaseorder { get; set; }
    }
}
