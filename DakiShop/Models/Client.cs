namespace DakiShop.Models
{
    public class Client
    {
        public Guid ID { get; set; }
        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string AvatarURL { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public ICollection<Review> Reviews { get; set; } = null!;
    }
}
