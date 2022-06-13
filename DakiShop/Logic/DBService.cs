using DakiShop.Models;

namespace DakiShop.Logic
{
    public static class DBService
    {
        public static void Init()
        {
            using(DBContext db = new DBContext())
            {
                db.Client.Add(new Client { IsAdmin = false, PasswordHash = "a", UserName = "d" });
                db.SaveChanges();
            }
        }
    }
}
