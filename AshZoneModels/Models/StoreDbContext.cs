using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AshZoneModels.Models
{
    public partial class StoreDbContext: DbContext
    {
        public StoreDbContext()
        {

        }
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=LAPTOP-1FG071R0\\MYSERVER; Database=ASHZONA; Trusted_Connection=True; MultipleActiveResultSets=True;");

        public virtual DbSet<Product> Products { get; set; }
       
        // Called when we're doing database migrations, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
