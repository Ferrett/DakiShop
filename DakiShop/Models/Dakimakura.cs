namespace DakiShop.Models
{
    public class Dakimakura
    {
        public int ID { get; set; }
        public Category Category { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public int Price { get; set; }
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string Filler { get; set; } = null!;
        public Manufacturer Manufacturer { get; set; } = null!;
        public decimal Rating { get; set; }
        public int PurchasedNumber { get; set; }
        public ICollection<Review> Reviews { get; set; } = null!;
    }
}
