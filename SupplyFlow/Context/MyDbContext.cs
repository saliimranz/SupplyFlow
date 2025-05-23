using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SupplyFlow.Models; // Assuming your models are in the Models namespace

namespace SupplyFlow.Context
{
    public class MyDbContext : DbContext
    {
        // Use the name of your connection string from Web.config (we'll check this in a second)
        public MyDbContext() : base("DefaultConnection") {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<ItemMaster> ItemMaster { get; set; }
        public DbSet<PurchaseOrders> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItems> PurchaseOrderItems { get; set; }
        public DbSet<PO_Actions> PO_Actions { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure decimal precision for PurchaseOrders
            modelBuilder.Entity<PurchaseOrders>()
                .Property(p => p.Total_Amount)
                .HasPrecision(18, 2);

            // Configure decimal precision for PurchaseOrderItems
            modelBuilder.Entity<PurchaseOrderItems>()
                .Property(p => p.Unit_Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseOrderItems>()
                .Property(p => p.Tax_Rate)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PurchaseOrderItems>()
                .Property(p => p.Total_Amount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}