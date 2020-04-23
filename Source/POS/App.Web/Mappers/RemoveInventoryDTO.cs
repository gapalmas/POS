using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Mappers
{
    public class RemoveInventoryDTO
    {
        public int Id { get; set; }
        public double Stock { get; set; }
        public int InventoryId { get; set; }
        public int CustomerId { get; set; }
    }
}
