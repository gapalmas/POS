namespace App.Web.Mappers
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public string Description { get; set; }
        public double Stock { get; set; }
        public double StockMin { get; set; }
        public double StockMax { get; set; }
        public string Location { get; set; }        
    }
}
