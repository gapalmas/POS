using App.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Infrastructure.Data
{
    public class Seed
    {
        public static void Start(ModelBuilder modelBuilder)
        {
            #region 'Users Identity'

            /*Seeder see : https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding */
            var password = "123456";

            var guillermo = new User
            {
                Id = "1",
                UserName = "gapalmasolano@gmail.com",
                NormalizedUserName = "GAPALMASOLANO@GMAIL.COM",
                Email = "gapalmasolano@gmail.com",
                NormalizedEmail = "gapalmasolano@gmail.com".ToUpper(),
                EmailConfirmed = true,
                FirstName = "Guillermo",
                LastName = "Palma"
            };
            guillermo.PasswordHash = new PasswordHasher<User>().HashPassword(guillermo, password);

            var admin = new User
            {
                Id = "2",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@inventse.com",
                NormalizedEmail = "admin@inventse.com".ToUpper(),
                EmailConfirmed = true,
                FirstName = "NameTest",
                LastName = "LastTest"
            };
            admin.PasswordHash = new PasswordHasher<User>().HashPassword(admin, password);

            modelBuilder.Entity<User>().HasData(guillermo, admin);


            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "9919cdf5-d72d-4cb2-83dc-34017017eed7", Name = "Admin", NormalizedName = "Admin".ToUpper() },
                new IdentityRole { Id = "5f6ee6bc-49ef-49ba-a7a2-271a1f2c9ce1", Name = "User", NormalizedName = "User".ToUpper() },
                new IdentityRole { Id = "297da0fc-170f-4ead-b04e-ee431efcbd63", Name = "Customer", NormalizedName = "Customer".ToUpper() });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "9919cdf5-d72d-4cb2-83dc-34017017eed7",
                UserId = "1"
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "9919cdf5-d72d-4cb2-83dc-34017017eed7",
                UserId = "2"
            });

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .HasData(
                    new IdentityUserClaim<string>
                    {
                        Id = 1,
                        UserId = "1",
                        ClaimType = "name",
                        ClaimValue = "Guillermo Palma"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 2,
                        UserId = "1",
                        ClaimType = "given_name",
                        ClaimValue = "Guillermo"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 3,
                        UserId = "1",
                        ClaimType = "family_name",
                        ClaimValue = "Palma"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 4,
                        UserId = "1",
                        ClaimType = "email",
                        ClaimValue = "gapalmasolano@gmail.com"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 5,
                        UserId = "1",
                        ClaimType = "website",
                        ClaimValue = "http://guillermo.com"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 6,
                        UserId = "2",
                        ClaimType = "name",
                        ClaimValue = "NameTest LastTest"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 7,
                        UserId = "2",
                        ClaimType = "given_name",
                        ClaimValue = "NameTest"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 8,
                        UserId = "2",
                        ClaimType = "family_name",
                        ClaimValue = "LastTest"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 9,
                        UserId = "2",
                        ClaimType = "email",
                        ClaimValue = "admin@gmail.com"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 10,
                        UserId = "2",
                        ClaimType = "website",
                        ClaimValue = "http://admin.com"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 11,
                        UserId = "1",
                        ClaimType = "email_verified",
                        ClaimValue = true.ToString()
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 12,
                        UserId = "2",
                        ClaimType = "email_verified",
                        ClaimValue = true.ToString()
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 13,
                        UserId = "1",
                        ClaimType = "address",
                        ClaimValue = @"{ 'street_address': 'One Way', 'locality': 'Juarez', 'postal_code': 32000, 'country': 'Mexico' }"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 14,
                        UserId = "2",
                        ClaimType = "address",
                        ClaimValue = @"{ 'street_address': 'One Way', 'locality': 'Juarez', 'postal_code': 32000, 'country': 'Mexico' }"
                    },
                    new IdentityUserClaim<string>
                    {
                        Id = 15,
                        UserId = "1",
                        ClaimType = "location",
                        ClaimValue = "somewhere"
                    });
            #endregion

            #region 'Category'
            modelBuilder.Entity<Category>().HasData(
                new Category() { Description = "HERRAMIENTAS", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Category() { Description = "FERRETERIA", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Category() { Description = "MADERA", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true }
                );
            #endregion

            #region 'Clasification'
            modelBuilder.Entity<Clasification>().HasData(
                new Clasification() { Description = "NEW", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Clasification() { Description = "HIGH", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Clasification() { Description = "SEASON", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Clasification() { Description = "LOW", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true }
                );
            #endregion

            #region 'Customer'
            modelBuilder.Entity<Customer>().HasData(
                new Customer() { CommercialName = "Cliente de prueba", BussinessName = "Cliente de prueba", Address = "Address", Cp = 32576, Rfc = "PASG840415NY3", DayCredit = 15, Status = true, Date = DateTime.Now, DateUpdate = DateTime.Now }
                );
            #endregion

            #region 'Inventory'
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory() { Stock = 0, StockMin = 0, StockMax = 0, Location = "", UnitId = 1, Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true, Equal = 0, WarehouseId = 1 }
                );
            #endregion

            #region 'Product'
            modelBuilder.Entity<Product>().HasData(
                new Product() { Description = "Producto de prueba", Price = decimal.Parse("10.5"), ClasificationId = 1, CategoryId = 1, InventoryId = 1, Status = true, Date = DateTime.Now, DateUpdate = DateTime.Now, ImagePath = "Imagen", PartNumber = "XXXXX" }
                );
            #endregion

            #region 'Supplier'
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier() { CommercialName = "Proveedor de prueba", BussinessName = "Proveedor de prueba", Address = "Address", Cp = 32576, Rfc = "PASG840415NY3", DayCredit = 15, Status = true, Date = DateTime.Now, DateUpdate = DateTime.Now }
                );
            #endregion

            #region 'Units'
            modelBuilder.Entity<Warehouse>().HasData(
                new Unit() { Description = "CAJA", Measure = "PIEZA", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true }
                );
            #endregion

            #region 'Warehouse'
            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse() { Description = "JUAREZ", Ubication = "CHIHUAHUA", CoCe = "CDJRZ", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true }
                );
            #endregion
        }
    }
}
