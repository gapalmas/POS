namespace App.Core.Entities
{
    public partial class Company : BaseEntity
    {
        public string CommercialName { get; set; }
        public string BussinessName { get; set; }
        public string Rfc { get; set; }
        public string Address { get; set; }
        public decimal Cp { get; set; }
    }
}
