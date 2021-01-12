using App.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace App.Web.DTOs
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Value { get { return this.Price * (decimal)Inventory.Stock; } }
        public virtual Inventory Inventory { get; set; }
    }
}
