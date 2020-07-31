﻿// <auto-generated />
using System;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200731054442_PercentOnCustomer")]
    partial class PercentOnCustomer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("App.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<string>("Description");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("App.Core.Entities.Clasification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<string>("Description");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Clasification");
                });

            modelBuilder.Entity("App.Core.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BussinessName");

                    b.Property<string>("CommercialName");

                    b.Property<decimal>("Cp");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<string>("Rfc");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("App.Core.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BussinessName");

                    b.Property<string>("CommercialName");

                    b.Property<decimal>("Cp");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<int>("DayCredit");

                    b.Property<double>("Percent");

                    b.Property<string>("Rfc");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("App.Core.Entities.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<double>("Equal");

                    b.Property<string>("Location");

                    b.Property<bool>("Status");

                    b.Property<double>("Stock");

                    b.Property<double>("StockMax");

                    b.Property<double>("StockMin");

                    b.Property<int>("UnitId");

                    b.Property<int>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("App.Core.Entities.Inventoryio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<int>("InventoryId");

                    b.Property<decimal?>("Price");

                    b.Property<double>("Quantity");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("Inventoryio");
                });

            modelBuilder.Entity("App.Core.Entities.Orderitemspays", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<int>("ProductId");

                    b.Property<int>("PurchaseOrderPayId");

                    b.Property<double>("Quantity");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("PurchaseOrderPayId");

                    b.ToTable("Orderitemspays");
                });

            modelBuilder.Entity("App.Core.Entities.Orderitemssales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<decimal>("Price");

                    b.Property<int>("ProductId");

                    b.Property<int>("PurchaseOrderId");

                    b.Property<double>("Quantity");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("Orderitemssales");
                });

            modelBuilder.Entity("App.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("ClasificationId");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<int>("InventoryId");

                    b.Property<string>("PartNumber");

                    b.Property<decimal>("Price");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ClasificationId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("App.Core.Entities.Purchaseorder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Confirm");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<bool>("Delivery");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Purchaseorder");
                });

            modelBuilder.Entity("App.Core.Entities.Purchaseorderpay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<bool>("Status");

                    b.Property<int>("SupplierId");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Purchaseorderpay");
                });

            modelBuilder.Entity("App.Core.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BussinessName");

                    b.Property<string>("CommercialName");

                    b.Property<decimal>("Cp");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<int>("DayCredit");

                    b.Property<string>("Rfc");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("App.Core.Entities.Ticketorder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<int>("PurchaseOrderId");

                    b.Property<bool>("Status");

                    b.Property<double>("Subtotal");

                    b.Property<double>("Tax");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("Ticketorder");
                });

            modelBuilder.Entity("App.Core.Entities.Ticketorderpay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<int>("PurchaseOrderPayId");

                    b.Property<bool>("Status");

                    b.Property<double>("Subtotal");

                    b.Property<double>("Tax");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseOrderPayId");

                    b.ToTable("Ticketorderpay");
                });

            modelBuilder.Entity("App.Core.Entities.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<string>("Description");

                    b.Property<string>("Measure");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("App.Core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("App.Core.Entities.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CoCe");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateUpdate");

                    b.Property<string>("Description");

                    b.Property<bool>("Status");

                    b.Property<string>("Ubication");

                    b.HasKey("Id");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("App.Core.Entities.Inventory", b =>
                {
                    b.HasOne("App.Core.Entities.Unit", "Unit")
                        .WithMany("Inventory")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("App.Core.Entities.Warehouse", "Warehouse")
                        .WithMany("Inventory")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Core.Entities.Inventoryio", b =>
                {
                    b.HasOne("App.Core.Entities.Inventory", "Inventory")
                        .WithMany("Inventoryio")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Core.Entities.Orderitemspays", b =>
                {
                    b.HasOne("App.Core.Entities.Product", "Product")
                        .WithMany("Orderitemspays")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("App.Core.Entities.Purchaseorderpay", "PurchaseOrderPay")
                        .WithMany("Orderitemspays")
                        .HasForeignKey("PurchaseOrderPayId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Core.Entities.Orderitemssales", b =>
                {
                    b.HasOne("App.Core.Entities.Product", "Product")
                        .WithMany("Orderitemssales")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("App.Core.Entities.Purchaseorder", "PurchaseOrder")
                        .WithMany("Orderitemssales")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Core.Entities.Product", b =>
                {
                    b.HasOne("App.Core.Entities.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("App.Core.Entities.Clasification", "Clasification")
                        .WithMany("Product")
                        .HasForeignKey("ClasificationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("App.Core.Entities.Inventory", "Inventory")
                        .WithMany("Product")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Core.Entities.Purchaseorder", b =>
                {
                    b.HasOne("App.Core.Entities.Customer", "Customer")
                        .WithMany("Purchaseorder")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Core.Entities.Purchaseorderpay", b =>
                {
                    b.HasOne("App.Core.Entities.Supplier", "Supplier")
                        .WithMany("Purchaseorderpay")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Core.Entities.Ticketorder", b =>
                {
                    b.HasOne("App.Core.Entities.Purchaseorder", "PurchaseOrder")
                        .WithMany("Ticketorder")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Core.Entities.Ticketorderpay", b =>
                {
                    b.HasOne("App.Core.Entities.Purchaseorderpay", "PurchaseOrderPay")
                        .WithMany("Ticketorderpay")
                        .HasForeignKey("PurchaseOrderPayId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("App.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("App.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("App.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("App.Core.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
