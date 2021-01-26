using System;

namespace App.Web.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
