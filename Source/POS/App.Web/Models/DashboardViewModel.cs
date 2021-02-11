using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class DashboardViewModel
    {
        public int Orders { get; set; }
        public int Deliveries { get; set; }
        public int Customers { get; set; }
        public int Products { get; set; }
    }
}
