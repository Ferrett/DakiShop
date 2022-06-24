namespace DakiShop.Models
{
    public class Dakimakura
    {
        public int ID { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual Manufacturer Manufacturer { get; set; } = null!;
        public string ImageURL { get; set; } = null!;
        public int Price { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Height{ get; set; }
        public int Width{ get; set; } 
        public double Rating { get; set; }
        public int PurchasedNumber { get; set; }
        public ICollection<Review> Reviews { get; set; } = null!;
    }
}
