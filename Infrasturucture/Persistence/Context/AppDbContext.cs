﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class AppDbContext : DbContext    //dbcontext kütüphanesini projemiz içinde bizim kendi db baglantı sınıfımız için kalıtım aldık.
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; database=ECommerce; Integrated Security=True; TrustServerCertificate=True;");               // kendi db miz için baglantı ayarları.
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Support> Support { get; set; }
        public DbSet<Contact> Contacts { get; set; }



    }
}
