namespace DakiShop.Models
{
    public class Review
    {
        public int ID { get; set; }
        public Dakimakura Dakimakura { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
        public Client Client { get; set; } = null!;
        public int Likes { get; set; }
        public DateTime ReviewDateTime { get; set; }
    }
}
