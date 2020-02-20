using System.Collections.Generic;

namespace App.Core.Entities
{
    public partial class Unit : BaseEntity
    {
        public Unit()
        {
            Inventory = new HashSet<Inventory>();
        }

        public string Description { get; set; }
        public string Measure { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
