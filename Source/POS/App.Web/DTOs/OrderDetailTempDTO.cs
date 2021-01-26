using App.Core.Entities;

namespace App.Web.DTOs
{
    public class OrderDetailTempDTO
    {
        public int Id;
        public User User { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public decimal Value { get; }
    }
}
