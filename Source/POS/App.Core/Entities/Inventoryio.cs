using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Entities
{
    public partial class Inventoryio : BaseEntity
    {
        public double Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Price { get; set; }
        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
