using System.Collections.Generic;

namespace App.Core.Entities
{
    public partial class Warehouse : BaseEntity
    {
        public Warehouse()
        {
            Inventory = new HashSet<Inventory>();
        }

        public string Description { get; set; }
        public string Ubication { get; set; }
        public string CoCe { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
