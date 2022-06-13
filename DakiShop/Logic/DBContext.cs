using DakiShop.Models;
using Microsoft.EntityFrameworkCore;

namespace DakiShop.Logic
{
    public class DBContext : DbContext
    {
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Manufacturer> Manufacturer { get; set; } = null!;
        public DbSet<Filler> Filler { get; set; } = null!;
        public DbSet<Client> Client { get; set; } = null!;
        public DbSet<Dakimakura> Dakimakura { get; set; } = null!;
        public DbSet<Review> Review { get; set; } = null!;

        public DBContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.configuration.GetConnectionString("MSSQL"));
        }
    }
}
