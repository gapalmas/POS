using System.Collections.Generic;

namespace App.Core.Entities
{
    public partial class Product : BaseEntity
    {
        public Product()
        {
            Orderitemspays = new HashSet<Orderitemspays>();
            Orderitemssales = new HashSet<Orderitemssales>();
        }

        public string Description { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string PartNumber { get; set; }
        public int CategoryId { get; set; }
        public int ClasificationId { get; set; }
        public int InventoryId { get; set; }

        //public string Description { get; set; }
        //public double Price { get; set; }
        //public string ImagePath { get; set; }
        //public string PartNumber { get; set; }
        //public int CategoryId { get; set; }
        //public int ClasificationId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Clasification Clasification { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual ICollection<Orderitemspays> Orderitemspays { get; set; }
        public virtual ICollection<Orderitemssales> Orderitemssales { get; set; }
    }
}
