using GameStore.DomainCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using GameStore.DomainCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameStore.DomainCore.Infrastructure
{
    public class GameStoreDBContext : IdentityDbContext<AppUser>
    {
        public GameStoreDBContext(DbContextOptions<GameStoreDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
