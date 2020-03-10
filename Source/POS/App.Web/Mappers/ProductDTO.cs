using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Mappers
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string PartNumber { get; set; }
        public int CategoryId { get; set; }
        public int ClasificationId { get; set; }
        public int InventoryId { get; set; }
    }
}
