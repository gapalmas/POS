using System.Collections.Generic;

namespace App.Core.Entities
{
    public partial class Inventory : BaseEntity
    {
        public Inventory()
        {
            Inventoryio = new HashSet<Inventoryio>();
        }

        public double Stock { get; set; }
        public double StockMin { get; set; }
        public double StockMax { get; set; }
        public double Equal { get; set; }
        public string Location { get; set; }
        public int UnitId { get; set; }
        public int WarehouseId { get; set; }


        //public double Stock { get; set; }
        //public double StockMin { get; set; }
        //public double StockMax { get; set; }
        //public double Equal { get; set; }
        //public string Location { get; set; }
        //public int UnitId { get; set; }
        //public int ProductId { get; set; }
        //public int WarehouseId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<Inventoryio> Inventoryio { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
