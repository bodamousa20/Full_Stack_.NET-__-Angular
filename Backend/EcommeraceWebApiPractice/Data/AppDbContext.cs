using Ecom.Core.Entites;
using EcommeraceWebApiPractice.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ecom.infrrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AccountUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> categories { set; get; }
        
        public DbSet<Product> products { set; get; }
        public DbSet<cart> cart { set; get; }
        public DbSet<cartItem> cartItem { set; get; }

        public DbSet<Photo> photos { set; get; }
        public DbSet<Favourite> favourites { set; get; }
        public DbSet<Order>Orders { set; get; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cart>().HasKey(p => p.Id);
            modelBuilder.Entity<cartItem>().HasKey(p => p.id);

            modelBuilder.Entity<cart>().HasOne(p => p.User).WithOne(p => p.Cart);
            modelBuilder.Entity<cartItem>()
                        .Property(ci => ci.price)
                       .HasPrecision(18, 2);

            modelBuilder.Entity<cartItem>().HasOne(p => p.Cart)
                                           .WithMany(P => P.CartItems)
                                           .HasForeignKey(P=>P.CartId);

            modelBuilder.Entity<cartItem>().HasOne(p => p.Product)
                                           .WithMany()
                                           .HasForeignKey(p => p.ProductId);



            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
