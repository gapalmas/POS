using App.Core.Entities;
using App.Infrastructure.Data;
using App.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Data
{
    public class Seeder
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        //private readonly Random random;

        public Seeder(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            //random = new Random();
        }

        public async Task SeederAsync()
        {
            await context.Database.EnsureCreatedAsync();

            /*Valida que los roles existan*/
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");


            var user = await userHelper.GetUserByEmailAsync("gapalmasolano@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Guillermo",
                    LastName = "Palma",
                    Email = "gapalmasolano@gmail.com",
                    UserName = "gapalmasolano@gmail.com",
                    PhoneNumber = "6565659048"
                };

                var result = await userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            await this.userHelper.AddUserToRoleAsync(user, "Admin");

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            if (!context.Clasification.Any())
            {
                context.Clasification.AddRange(
                new Clasification() { Description = "NEW", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Clasification() { Description = "HIGH", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Clasification() { Description = "SEASON", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Clasification() { Description = "LOW", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true }
                );
            }
            context.SaveChanges();
            if (!context.Category.Any())
            {
                context.Category.AddRange(
                new Category() { Description = "HERRAMIENTAS", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Category() { Description = "FERRETERIA", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true },
                new Category() { Description = "MADERA", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true }
                );
            }
            context.SaveChanges();
            if (!context.Warehouse.Any())
            {
                context.Warehouse.AddRange(
                new Warehouse() { Description = "JUAREZ", Ubication = "CHIHUAHUA", CoCe = "CDJRZ", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true }
                );
            }
            context.SaveChanges();
            if (!context.Unit.Any())
            {
                context.Unit.Add(
                new Unit() { Description = "CAJA", Measure = "PIEZA", Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true }
                );
                context.SaveChanges();
            }

            if (!context.Inventory.Any())
            {
                context.Inventory.Add(
                new Inventory() { Stock = 0, StockMin = 0, StockMax = 0, Location = "", UnitId = 1, Date = DateTime.Now, DateUpdate = DateTime.Now, Status = true, Equal = 0, WarehouseId = 1 }
                );
                context.SaveChanges();
            }

            if (!context.Product.Any())
            {
                context.Product.Add(
                new Product() { Description = "Producto de prueba", Price = decimal.Parse("10.5"), ClasificationId = 1, CategoryId = 1, InventoryId = 1, Status = true, Date = DateTime.Now, DateUpdate = DateTime.Now, ImagePath = "Imagen", PartNumber = "XXXXX" }
                );
                context.SaveChanges();
            }

            if (!context.Customer.Any())
            {
                context.Customer.Add(
                new Customer() { CommercialName = "Cliente de prueba", BussinessName = "Cliente de prueba", Address = "Address", Cp = 32576, Rfc = "PASG840415NY3", DayCredit = 15, Status = true, Date = DateTime.Now, DateUpdate = DateTime.Now }
                );
                context.SaveChanges();
            }

            if (!context.Supplier.Any())
            {
                context.Supplier.Add(
                new Supplier() { CommercialName = "Proveedor de prueba", BussinessName = "Proveedor de prueba", Address = "Address", Cp = 32576, Rfc = "PASG840415NY3", DayCredit = 15, Status = true, Date = DateTime.Now, DateUpdate = DateTime.Now }
                );
                context.SaveChanges();
            }
        }
    }
}