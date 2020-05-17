using App.Core.Entities;

namespace App.Web.Mappers
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get { return this.Price * (decimal)Inventory.Stock; } }
        public virtual Inventory Inventory { get; set; }
    }
}
