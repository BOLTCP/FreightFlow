using FreightFlow.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreightFlow.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<FreightInventories> FreightInventory { get; set; }
        public DbSet<Equipments> Equipment { get; set; }
        public object FreightInventories { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FreightInventories>().HasKey(e => e.DeliveryId);
            modelBuilder.Entity<FreightInventories>().ToTable("FreightInventory");
        }
    }


}
