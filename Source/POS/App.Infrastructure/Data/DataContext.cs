using App.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace App.Infrastructure.Data
{
    /*Use header for IdentityDbContext for create & use model*/
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #region 'DbSet Entities'
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Clasification> Clasification { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Inventoryio> Inventoryio { get; set; }
        public virtual DbSet<Orderitemspays> Orderitemspays { get; set; }
        public virtual DbSet<Orderitemssales> Orderitemssales { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Purchaseorder> Purchaseorder { get; set; }
        public virtual DbSet<Purchaseorderpay> Purchaseorderpay { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Ticketorder> Ticketorder { get; set; }
        public virtual DbSet<Ticketorderpay> Ticketorderpay { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*
                 * To protect potentially sensitive information in your connection string, you should move it out of source code. 
                 * See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                 */
                //optionsBuilder.UseMySql("server=localhost;user id=root;password=;database=app_pos;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Reference: https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete
            
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t
                .GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
            /*Load All Configurations on Project */
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            /*Call Seeder */
            //Seed.Start(modelBuilder);
        }
    }
}
