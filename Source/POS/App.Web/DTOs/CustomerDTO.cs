namespace App.Web.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string CommercialName { get; set; }
        public string BussinessName { get; set; }
        public string Rfc { get; set; }
        public decimal Cp { get; set; }
        public string Address { get; set; }
        public int DayCredit { get; set; }
        public int Percent { get; set; }
    }
}
