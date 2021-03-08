using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Common.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        //[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public string ImagePath { get; set; }
        //public string PartNumber { get; set; }
        //public int CategoryId { get; set; }
        //public int ClasificationId { get; set; }
        //public int InventoryId { get; set; }
    }
}
