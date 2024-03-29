﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public string PartNumber { get; set; }
        public int CategoryId { get; set; }
        public int ClasificationId { get; set; }
        public int InventoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Clasification Clasification { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual ICollection<Orderitemspays> Orderitemspays { get; set; }
        public virtual ICollection<Orderitemssales> Orderitemssales { get; set; }
    }
}
