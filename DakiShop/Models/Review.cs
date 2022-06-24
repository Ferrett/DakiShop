namespace DakiShop.Models
{
    public class Review
    {
        public int ID { get; set; }
        public virtual Dakimakura Dakimakura { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime PublishTime { get; set; }
    }
}
