namespace DakiShop.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
