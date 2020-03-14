using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using App.Core.Entities;
using App.Infrastructure.Data;
using App.Web.Helpers;
using App.Web.Data;
using AutoMapper;
using System;
using App.Core.Interfaces;
using App.Core.UseCases;

namespace App.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // //Auto Mapper Configurations
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});

            // IMapper mapper = mappingConfig.CreateMapper();
            // services.AddSingleton(mapper);

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                /*Reference:
                 * https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.2&tabs=visual-studio
                 */
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<DataContext>();

            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseMySql(this.Configuration.GetConnectionString("MySQLConnection"));
                //cfg.UseSqlServer(this.Configuration.GetConnectionString("SQLServerConnection"));
            });

            services.AddAuthentication().AddCookie().AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = this.Configuration["Tokens:Issuer"],
                    ValidAudience = this.Configuration["Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Tokens:Key"]))
                };
            });

            
            services.AddTransient<Seeder>();



            /*Startup*/
            /*DI for Product*/
            services.AddScoped<IOperations<Product>, ManageOperations<Product>>();
            services.AddScoped<IRepository<Product>, GenericRepository<Product>>();
            /*DI for Product*/
            services.AddScoped<IOperations<Inventory>, ManageOperations<Inventory>>();
            services.AddScoped<IRepository<Inventory>, GenericRepository<Inventory>>();
            /*DI for Product*/
            services.AddScoped<IOperations<Customer>, ManageOperations<Customer>>();
            services.AddScoped<IRepository<Customer>, GenericRepository<Customer>>();




            //services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUserHelper, UserHelper>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/NotAuthorized";
                options.AccessDeniedPath = "/Account/NotAuthorized";
            });

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
