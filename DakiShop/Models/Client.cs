namespace DakiShop.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string AvaPath { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
