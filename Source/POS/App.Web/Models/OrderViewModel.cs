using App.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class OrderViewModel : PurchaseDTO
    {
        public int Customer_Id { get; set; }
        public string CommercialName { get; set; }
        public string BussinessName { get; set; }
    }
}
