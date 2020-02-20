using System.Collections.Generic;

namespace App.Core.Entities
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public string Description { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
