namespace DakiShop.Models
{
    public class ItemInCart
    {
        public int ID { get; set; }
        public virtual Client Client { get; set; } = null!;
        public virtual Dakimakura Dakimakura { get; set; } = null!;
    }
}
