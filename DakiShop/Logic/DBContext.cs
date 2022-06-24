using DakiShop.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
namespace DakiShop.Logic
{
    public class DBContext : DbContext
    {
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Manufacturer> Manufacturer { get; set; } = null!;
        public DbSet<Client> Client { get; set; } = null!;
        public DbSet<Dakimakura> Dakimakura { get; set; } = null!;
        public DbSet<Review> Review { get; set; } = null!;
        public DbSet<ItemInCart> ItemInCart{ get; set; } = null!;
        public DbSet<ReviewLike> ReviewLike { get; set; } = null!;

        public DBContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.configuration.GetConnectionString("MYSQL"), new MySqlServerVersion(new Version(10,6,7))); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Publisher>(entity =>
            //{
            //    entity.HasKey(e => e.ID);
            //    entity.Property(e => e.Name).IsRequired();
            //});

            //modelBuilder.Entity<Book>(entity =>
            //{
            //    entity.HasKey(e => e.ISBN);
            //    entity.Property(e => e.Title).IsRequired();
            //    entity.HasOne(d => d.Publisher)
            //      .WithMany(p => p.Books);
            //});
        }

    }
}
