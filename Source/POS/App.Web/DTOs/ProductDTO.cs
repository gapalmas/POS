using App.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace App.Web.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string PartNumber { get; set; }
        public int CategoryId { get; set; }
        public int ClasificationId { get; set; }
        public int InventoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
