using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Entities
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }
        [Display(Name = "Name")]
        public string Description { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
