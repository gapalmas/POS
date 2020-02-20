using App.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #region 'Entities'
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Category>().HasData(
            //    new Core.Entities.Category() { Description = "HERRAMIENTAS", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
            //    new Core.Entities.Category() { Description = "FERRETERIA", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
            //    new Core.Entities.Category() { Description = "MADERA", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true }

            //    );

            /*builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");*/
            /*Reference: https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete
             */
            var cascadeFKs = builder.Model
                .GetEntityTypes()
                .SelectMany(t => t
                .GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
    }
}
