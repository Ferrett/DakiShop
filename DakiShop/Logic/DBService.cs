using DakiShop.Models;

namespace DakiShop.Logic
{
    public static class DBService
    {
        public static List<Dakimakura> dakimakuras { get; set; } = new List<Dakimakura>();
        public static List<Category> categories { get; set; } = new List<Category>();
        public static void InitDB()
        {
            using (DBContext db = new DBContext())
            {
                db.SaveChanges();
            }
        }

        public static void AddDakimakuraToDB(int categoryID, string imagePath, string name, int price, string size, int fillerID, int manufacturerID)
        {
            using (DBContext db = new DBContext())
            {
                db.Dakimakura.Add(new Dakimakura
                {
                    Category = db.Category.FirstOrDefault(x=>x.ID==categoryID)!,
                    ImagePath =imagePath ,
                    Name = name,
                    Price = price,
                    Size = size,
                    Filler = db.Filler.FirstOrDefault(x => x.ID == fillerID)!,
                    Manufacturer = db.Manufacturer.FirstOrDefault(x => x.ID ==manufacturerID )!,
                    Rating = 0
                });
                db.SaveChanges();
            }
        }

        public static void AddRootUser(string login, string email, string password)
        {
            using (DBContext db = new DBContext())
            {
                db.Client.Add(new Client { Login = login.ToLower(), Email = email.ToLower(), PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt()), IsAdmin = true });
                db.SaveChanges();
            }
        }

        public static bool GetCurrentUserRoot(string login)
        {
            using (DBContext db = new DBContext())
            {
                return db.Client.ToList().Find(x => x.Login.Equals(login))!.IsAdmin;
            } 
        }
        public static Category GetCategoryByName(string name)
        {
            using (DBContext db = new DBContext())
            {
                return db.Category.ToList().Find(x=>x.Name.Equals(name))!;
            }
        }

        public static Filler GetFillerByName(string name)
        {
            using (DBContext db = new DBContext())
            {
                return db.Filler.ToList().Find(x => x.Name.Equals(name))!;
            }
        }

        public static Manufacturer GetManufacturerByName(string name)
        {
            using (DBContext db = new DBContext())
            {
                return db.Manufacturer.ToList().Find(x => x.Name.Equals(name))!;
            }
        }

        public static void GetDataFromDB()
        {
            using(DBContext db = new DBContext())
            {
                dakimakuras = db.Dakimakura.ToList();
                categories = db.Category.ToList();
            }
        }

        public static void AddNewUserToDB(string login, string email, string password)
        {
            using (DBContext db = new DBContext())
            {
                db.Client.Add(new Client { Login = login.ToLower(), Email = email.ToLower(), PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt()), IsAdmin = false });
                db.SaveChanges();
            }
        }
        public static bool IsUserExists(string login)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Client.ToList().Any(x => x.Login == login.ToLower()))
                    return true;
            }
            return false;
        }

        public static bool UserLogIn(string login, string password)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Client.ToList().Any(x => x.Login == login.ToLower() && BCrypt.Net.BCrypt.Verify(password, x.PasswordHash)))
                    return true;
            }
            return false;
        }

        public static bool IsLoginTaken(string login)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Client.ToList().Any(x => x.Login == login))
                    return true;
            }
            return false;
        }

        public static bool IsEmailTaken(string email)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Client.ToList().Any(x => x.Email == email))
                    return true;
            }
            return false;
        }
    }
}
