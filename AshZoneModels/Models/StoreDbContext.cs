using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace AshZoneModels.Models
{
    public partial class StoreDbContext:IdentityDbContext<AppUser>
    {
        public StoreDbContext()
        {

        }
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=LAPTOP-1FG071R0\\MYSERVER; Database=ASHZONE; Trusted_Connection=True; MultipleActiveResultSets=True;");

        public virtual DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<ShopingCart> ShoppingCart { get; set; }

        public DbSet<OrderHeader> OrderHeaders{ get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        // Called when we're doing database migrations, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
