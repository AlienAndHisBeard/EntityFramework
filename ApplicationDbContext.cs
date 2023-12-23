using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasDiscriminator<char>("Type")
                .HasValue<Client>('N')
                .HasValue<EClient>('E');

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Client)
                .HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<Order>()
                .HasMany(c => c.OrderEntries)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<EOrder>()
                .HasMany(c => c.OrderEntries)
                .WithOne(o => o.Order as EOrder)
                .HasForeignKey(o => o.OrderId);

            base.OnModelCreating(modelBuilder);

            // generate fake data for the shop
            Randomizer.Seed = new Random(8675309);

            FakeData.Init(10);

            modelBuilder.Entity<Item>().HasData(FakeData.Items);
            modelBuilder.Entity<Client>().HasData(FakeData.Clients);
            modelBuilder.Entity<EClient>().HasData(FakeData.EClients);
            modelBuilder.Entity<OrderEntry>().HasData(FakeData.OrderEntries);
            modelBuilder.Entity<Order>().HasData(FakeData.Orders);
            modelBuilder.Entity<EOrder>().HasData(FakeData.EOrders);

        }
        public DbSet<Item> Items { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<EClient> EClients { get; set; }

        public DbSet<OrderEntry> OrderEntries { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<EOrder> EOrders { get; set; }


        /// <summary>
        /// Connection string hardcoded only becaouse of the specifications of the laboratory task
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true");
            options.EnableSensitiveDataLogging();
        }
    }
}
