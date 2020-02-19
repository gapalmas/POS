using App.Core.Entities;
using App.Infrastructure.Data;
using App.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
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


            //if (!context.Products.Any())
            //{
            //    AddProduct("iPhone X", user);
            //    AddProduct("Mouse", user);
            //    AddProduct("Samsung S10", user);
            //    await context.SaveChangesAsync();
            //}
        }

        //private void AddProduct(string name, User user)
        //{
        //    context.Products.Add(new Product
        //    {
        //        Name = name,
        //        Price = random.Next(100),
        //        IsAvailabe = true,
        //        Stock = random.Next(100),
        //        User = user
        //    });
        //}
    }
}
