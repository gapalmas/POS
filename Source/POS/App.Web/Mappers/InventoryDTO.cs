using App.Core.Entities;

namespace App.Web.Mappers
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public string Description { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
