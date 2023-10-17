using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.DAL.Entities;
using RestaurantApp.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.DAL.Database
{
    public  class DataContext:IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> opt):base(opt)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductOrder>().HasKey(a => new { a.Order_id, a.Product_id });
            builder.Entity<IdentityUserLogin<string>>().HasKey(a => a.ProviderKey);
            builder.Entity<IdentityUserRole<string>>().HasNoKey();
            builder.Entity<IdentityUserToken<string>>().HasNoKey();
            builder.Entity<Customer>().HasMany<Order>(a=>a.Orders).WithOne(a=>a.customer).OnDelete(deleteBehavior
                : DeleteBehavior.SetNull);
            builder.Entity<Worker>().HasMany<Order>(a => a.orders).WithOne(a => a.worker).OnDelete(deleteBehavior
                : DeleteBehavior.SetNull);
            //builder.Entity<Order>().HasMany<ProductOrder>(a => a.ProductOrders).WithOne(a => a.Order).OnDelete(deleteBehavior: DeleteBehavior.SetNull);
            //builder.Entity<Product>().HasMany<ProductOrder>(a => a.ProductOrders).WithOne(a => a.Product).OnDelete(deleteBehavior: DeleteBehavior.SetNull);

        }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
