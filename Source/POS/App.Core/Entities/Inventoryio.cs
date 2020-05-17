using System.ComponentModel.DataAnnotations;

namespace App.Core.Entities
{
    public partial class Inventoryio : BaseEntity
    {
        public double Quantity { get; set; }
        public decimal? Price { get; set; }
        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
