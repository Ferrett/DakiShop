using DakiShop.Models;
using Microsoft.EntityFrameworkCore;

namespace DakiShop.Logic
{
    public static class DBService
    {
        public static List<Dakimakura> dakimakuras { get; set; } = new List<Dakimakura>();
        public static List<Category> categories { get; set; } = new List<Category>();
        public static List<Manufacturer> manufacturers { get; set; } = new List<Manufacturer>();
        public static List<Review> reviews { get; set; } = new List<Review>();
        public static List<Client> clients { get; set; } = new List<Client>();

        public static void InitDB()
        {
            using (DBContext db = new DBContext())
            {
                UpdateData();
                db.SaveChanges();
            }
        }

        public static void AddDakimakura(int categoryID, string imagePath, string name, int price, string size, string filler, int manufacturerID)
        {
            using (DBContext db = new DBContext())
            {
                db.Dakimakura.Add(new Dakimakura
                {
                    Category = db.Category.FirstOrDefault(x => x.ID == categoryID)!,
                    ImagePath = imagePath,
                    Name = name,
                    Price = price,
                    Size = size,
                    Filler = filler,
                    Manufacturer = db.Manufacturer.FirstOrDefault(x => x.ID == manufacturerID)!,
                    PurchasedNumber = 0,
                    Rating = 0
                });
                db.SaveChanges();
            }
            UpdateData();
        }

        public static void EditDakimakura(int dakiID, int categoryID, string imagePath, string name, int price, string size, string filler, int manufacturerID)
        {
            using (DBContext db = new DBContext())
            {
                var d = db.Dakimakura.Where(x => x.ID == dakiID).First();
                d.Category = db.Category.FirstOrDefault(x => x.ID == categoryID)!;
                d.ImagePath = imagePath;
                d.Name = name;
                d.Price = price;
                d.Size = size;
                d.Filler = filler;
                d.Manufacturer = db.Manufacturer.FirstOrDefault(x => x.ID == manufacturerID)!;


                db.SaveChanges();
            }
            UpdateData();
        }


        public static void AddReview(int dakiID, string text, int rating, string userName)
        {
            using (DBContext db = new DBContext())
            {
                db.Review.Add(new Review
                {
                    Dakimakura = db.Dakimakura.Where(x => x.ID == dakiID).First(),
                    Text = text,
                    Rating = rating,
                    ReviewDateTime = DateTime.Now,
                    Likes = 0,
                    Client = db.Client.FirstOrDefault(x => x.Login.ToLower().Equals(userName.ToLower()))!
                });
                db.SaveChanges();
            }

            UpdateData();
        }
        public static void AddRootUser(string login, string email, string password, string avaPath)
        {
            using (DBContext db = new DBContext())
            {
                db.Client.Add(new Client
                {
                    Login = login.ToLower(),
                    Email = email.ToLower(),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt()),
                    IsAdmin = true,
                    AvaPath = avaPath
                });
                db.SaveChanges();
            }
            UpdateData();
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
                return db.Category.ToList().Find(x => x.Name.Equals(name))!;
            }
        }

        public static Manufacturer GetManufacturerByName(string name)
        {
            using (DBContext db = new DBContext())
            {
                return db.Manufacturer.ToList().Find(x => x.Name.Equals(name))!;
            }
        }

        public static void UpdateData()
        {
            using (DBContext db = new DBContext())
            {
                dakimakuras = db.Dakimakura.ToList();
                categories = db.Category.ToList();
                manufacturers = db.Manufacturer.ToList();
                reviews = db.Review.ToList();
                clients = db.Client.ToList();
            }
        }

        public static void AddNewUser(string login, string email, string password)
        {
            using (DBContext db = new DBContext())
            {
                db.Client.Add(new Client { Login = login, Email = email.ToLower(), PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt()), IsAdmin = false, AvaPath= "https://dakisource.s3.eu-north-1.amazonaws.com/ava/1233.jpg" });
                db.SaveChanges();
            }
            UpdateData();
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
                if (db.Client.ToList().Any(x => x.Login.ToLower() == login.ToLower() && BCrypt.Net.BCrypt.Verify(password, x.PasswordHash)))
                    return true;
            }

            return false;
        }

        public static bool IsLoginTaken(string login)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Client.ToList().Any(x => x.Login.ToLower() == login.ToLower()))
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

        public static int GetMaxPriceInCategory(int categoryID)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Dakimakura.ToList().Count > 0)
                {
                    if (categoryID == 0)
                        return db.Dakimakura.ToList().MaxBy(x => x.Price).Price;

                    if (db.Dakimakura.Where(x => x.Category.ID == categoryID).Count() == 0)
                        return 0;

                    return db.Dakimakura.Where(x => x.Category.ID == categoryID).ToList().MaxBy(x => x.Price).Price;
                }
                else
                    return 0;
            }
        }

        public static bool IsCategoryExists(string name)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Category.ToList().Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    return true;
            }
            return false;
        }
        public static void AddCategory(string name)
        {
            using (DBContext db = new DBContext())
            {
                db.Category.Add(new Category { Name = name });
                db.SaveChanges();
            }
            UpdateData();
        }
        public static bool IsDakimakuraExists(string name)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Dakimakura.ToList().Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    return true;
            }
            return false;
        }

        public static bool IsDakimakuraExists(int id)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Dakimakura.ToList().Any(x => x.ID == id))
                    return true;
            }
            return false;
        }

        public static bool IsManufacturerExists(string name)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Manufacturer.ToList().Any(x => x.Name.ToLower().Equals(name.ToLower())))
                    return true;
            }
            return false;
        }
        public static void AddManufacturer(string name)
        {
            using (DBContext db = new DBContext())
            {
                db.Manufacturer.Add(new Manufacturer { Name = name });
                db.SaveChanges();
            }
            UpdateData();
        }

        public static bool IsManufacturerUsed(int id)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Dakimakura.Any(x => x.Manufacturer.ID == id))
                    return true;

                return false;
            }
        }

        public static void DeleteManufacturer(int id)
        {
            using (DBContext db = new DBContext())
            {
                db.Manufacturer.Remove(db.Manufacturer.Where(x => x.ID == id).First());

                db.SaveChanges();
            }
            UpdateData();
        }

        public static bool IsCategoryUsed(int id)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Dakimakura.Any(x => x.Category.ID == id))
                    return true;

                return false;
            }
            
        }

        public static void DeleteCategory(int id)
        {
            using (DBContext db = new DBContext())
            {
                db.Category.Remove(db.Category.Where(x => x.ID == id).First());



                db.SaveChanges();
            }
            UpdateData();
        }

        public static void DeleteDakimakura(int id)
        {
            using (DBContext db = new DBContext())
            {
                db.Dakimakura.Remove(db.Dakimakura.Where(x => x.ID == id).First());



                db.SaveChanges();
            }
            UpdateData();
        }

        public static void UpdateDakiRating(int dakiID)
        {
            using (DBContext db = new DBContext())
            {
                var d = db.Dakimakura.Where(x => x.ID == dakiID).First();


                decimal a = db.Review.Where(x => x.Dakimakura.ID == dakiID).Sum(x => x.Rating);
                decimal b = db.Review.Where(x => x.Dakimakura.ID == dakiID).Count();
                decimal c = a / b;
                d.Rating = c;
                db.SaveChanges();
            }
            UpdateData();
        }

        public static void DeleteReview(int reviewID)
        {
            using (DBContext db = new DBContext())
            {
                db.Review.Remove(db.Review.Where(x => x.ID == reviewID).First());

                db.SaveChanges();
            }
            UpdateData();
        }
    }
}
