namespace DakiShop.Models
{
    public class ReviewLike
    {
        public int ID { get; set; }
        public virtual Client Client { get; set; } = null!;
        public virtual Review Review{ get; set; } = null!;
    }
}
