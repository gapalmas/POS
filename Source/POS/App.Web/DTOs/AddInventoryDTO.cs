using System.ComponentModel.DataAnnotations;

namespace App.Web.DTOs
{
    public class AddInventoryDTO
    {
        public int Id { get; set; }
        public double Stock { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }
    }
}
