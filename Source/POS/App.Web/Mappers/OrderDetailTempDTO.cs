using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Mappers
{
    public class OrderDetailTempDTO
    {
        public int Id;
        public User User { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public decimal Value { get; }
    }
}
