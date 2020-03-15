using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Mappers
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string CommercialName { get; set; }
        public string BussinessName { get; set; }
        public string Rfc { get; set; }
        public decimal Cp { get; set; }
        public string Address { get; set; }
        public int DayCredit { get; set; }
    }
}
