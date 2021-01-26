namespace App.Web.DTOs
{
    public class RemoveInventoryDTO
    {
        public int Id { get; set; }
        public double Stock { get; set; }
        public decimal Price { get; set; }
        public int InventoryId { get; set; }
        public int CustomerId { get; set; }
    }
}
