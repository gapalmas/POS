using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Entities
{
    public partial class Supplier : BaseEntity
    {
        public Supplier()
        {
            Purchaseorderpay = new HashSet<Purchaseorderpay>();
        }
        public string CommercialName { get; set; }
        public string BussinessName { get; set; }
        public string Rfc { get; set; }
        [RegularExpression(@"^\d+\.\d{0,0}$")]
        [Column(TypeName = "decimal(5,0)")]
        public decimal Cp { get; set; }
        public string Address { get; set; }
        public int DayCredit { get; set; }
        public virtual ICollection<Purchaseorderpay> Purchaseorderpay { get; set; }
    }
}
