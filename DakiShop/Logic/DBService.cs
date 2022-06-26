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
        public static List<ItemInCart> itemsInCart { get; set; } = new List<ItemInCart>();
        public static List<ReviewLike> reviewLike { get; set; } = new List<ReviewLike>();

        public static void InitDB()
        {
            using (DBContext db = new DBContext())
            {
                UpdateData();
                db.SaveChanges();
            }
        }

        public static void AddDakimakura(int categoryID, string imageURL, string name, int price, int heigth, int width, string description, int manufacturerID)
        {
            using (DBContext db = new DBContext())
            {
                db.Dakimakura.Add(new Dakimakura
                {
                    Category = db.Category.FirstOrDefault(x => x.ID == categoryID)!,
                    ImageURL = imageURL,
                    Name = name,
                    Price = price,
                    Height = heigth,
                    Width = width,
                    Description = description,
                    Manufacturer = db.Manufacturer.FirstOrDefault(x => x.ID == manufacturerID)!,
                    PurchasedNumber = 0,
                    Rating = 0
                });
                db.SaveChanges();
            }
            UpdateData();
        }

        public static void EditDakimakura(int dakiID, int categoryID, string imageURL, string name, int price, int heigth, int width, string description, int manufacturerID)
        {
            using (DBContext db = new DBContext())
            {
                var d = db.Dakimakura.Where(x => x.ID == dakiID).First();
                d.Category = db.Category.FirstOrDefault(x => x.ID == categoryID)!;
                d.ImageURL = imageURL;
                d.Name = name;
                d.Price = price;
                d.Height = heigth;
                d.Width = width;
                d.Description = description;
                d.Manufacturer = db.Manufacturer.FirstOrDefault(x => x.ID == manufacturerID)!;


                db.SaveChanges();
            }
            UpdateData();
        }


        public static void AddReview(int dakiID, string text, int rating, Guid userID)
        {
            using (DBContext db = new DBContext())
            {
                db.Review.Add(new Review
                {
                    Dakimakura = db.Dakimakura.Where(x => x.ID == dakiID).First(),
                    Text = text,
                    Rating = rating,
                    PublishTime = DateTime.Now,
                    Client = db.Client.FirstOrDefault(x => x.ID.Equals(userID))!
                });
                db.SaveChanges();
            }

            UpdateData();
        }
        public static void AddRootUser(string login, string email, string password, string avatarURL)
        {
            using (DBContext db = new DBContext())
            {
                db.Client.Add(new Client
                {
                    Login = login.ToLower(),
                    Email = email.ToLower(),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password.ToLower(), BCrypt.Net.BCrypt.GenerateSalt()),
                    IsAdmin = true,
                    AvatarURL = avatarURL
                });
                db.SaveChanges();
            }
            UpdateData();
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
                itemsInCart = db.ItemInCart.ToList();
                reviewLike = db.ReviewLike.ToList();
            }
        }

        public static void AddNewUser(string login, string email, string password)
        {
            using (DBContext db = new DBContext())
            {
                db.Client.Add(new Client { Login = login, Email = email.ToLower(), PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt()), IsAdmin = false, AvatarURL = "https://dakisource.s3.eu-north-1.amazonaws.com/ava/1233.jpg" });
                db.SaveChanges();
            }
            UpdateData();
        }
        public static bool IsUserExists(Guid userID)
        {
            using (DBContext db = new DBContext())
            {
                return db.Client.ToList().Any(x => x.ID.Equals(userID))?  true: false;
            }
        }

        public static bool UserLogIn(string login, string password)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Client.ToList().Any(x => x.Login.ToLower().Equals(login.ToLower()) && BCrypt.Net.BCrypt.Verify(password.ToLower(), x.PasswordHash)))
                    return true;
            }

            return false;
        }

        public static bool IsLoginTaken(string login)
        {
            using (DBContext db = new DBContext())
            {
                return db.Client.ToList().Any(x => x.Login.ToLower().Equals(login.ToLower()))? true: false;  
            }
        }

        public static bool IsEmailTaken(string email)
        {
            using (DBContext db = new DBContext())
            {
                if (db.Client.ToList().Any(x => x.Email.ToLower().Equals(email)))
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


                double a = db.Review.Where(x => x.Dakimakura.ID == dakiID).Sum(x => x.Rating);
                double b = db.Review.Where(x => x.Dakimakura.ID == dakiID).Count();
                double c = a / b;
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

        public static void EditManufacturer(int manufactuerID, string newName)
        {
            using (DBContext db = new DBContext())
            {
                var d = db.Manufacturer.Where(x => x.ID == manufactuerID).First();

                d.Name = newName;
                db.SaveChanges();
            }
            UpdateData();
        }
        public static void EditCategory(int categoryID, string newName)
        {
            using (DBContext db = new DBContext())
            {
                var d = db.Category.Where(x => x.ID == categoryID).First();

                d.Name = newName;
                db.SaveChanges();
            }
            UpdateData();
        }

        public static async void AddItemToCart(Guid userID, int itemID)
        {
            await Task.Run(() =>
            {
                using (DBContext db = new DBContext())
                {
                    db.ItemInCart.Add(new ItemInCart
                    {
                        Client = db.Client.First(x => x.ID == userID),
                        Dakimakura = db.Dakimakura.First(x => x.ID == itemID),
                    });
                    db.SaveChanges();
                }
            });
        }

        public static async void DeleteItemFromCart(Guid userID, int itemID)
        {
            await Task.Run(() =>
            {
                using (DBContext db = new DBContext())
                {
                    db.ItemInCart.Remove(db.ItemInCart.Where(x => x.Client == db.Client.First(x => x.ID == userID) && x.Dakimakura == db.Dakimakura.First(x => x.ID == itemID)).First());

                    db.SaveChanges();
                }
            });
        }

        public static async void DeleteAllCart(Guid userID)
        {
            await Task.Run(() =>
            {
                using (DBContext db = new DBContext())
                {
                    db.ItemInCart.RemoveRange(db.ItemInCart.Where(x => x.Client == db.Client.First(x => x.ID == userID)));

                    db.SaveChanges();
                }
            });
        }

        public static async void AddBuys(Guid userID)
        {
            await Task.Run(() =>
            {
                using (DBContext db = new DBContext())
                {
                    List<ItemInCart> items = db.ItemInCart.Where(x => x.Client == db.Client.Where(x => x.ID == userID)).ToList();

                    List<Dakimakura> daki = new List<Dakimakura>();
                    foreach (var item in items)
                    {
                        daki.Add(db.Dakimakura.Where(x => x.ID == item.Dakimakura.ID).First());
                    }

                    daki.ForEach(x => x.PurchasedNumber++);
                    db.SaveChanges();
                }
            });
            UpdateData();
        }

        public static async void AddLike(Guid userID, int reviewID)
        {
            await Task.Run(() =>
            {
                using (DBContext db = new DBContext())
                {
                    db.ReviewLike.Add(new ReviewLike
                    {
                        Client = db.Client.First(x => x.ID == userID),
                        Review = db.Review.First(x => x.ID == reviewID),
                    });


                    db.SaveChanges();
                }
                //UpdateData();
            });
        }

        public static async void DeleteLike(Guid userID, int reviewID)
        {
            await Task.Run(() =>
            {
                using (DBContext db = new DBContext())
                {
                    db.ReviewLike.Remove(db.ReviewLike.Where(x => x.Client == db.Client.First(x => x.ID == userID) && x.Review == db.Review.First(x => x.ID == reviewID)).First());

                    db.SaveChanges();
                }
            });
        }

        public static bool IsRootUser(Guid userID)
        {
            using (DBContext db = new DBContext())
            {
                return db.Client.Any(x => x.ID.Equals(userID)) ? db.Client.First(x => x.ID.Equals(userID)).IsAdmin : false;
               // return db.Client.First(x => x.ID.Equals(userID)).IsAdmin;
            }
        }

        public static Guid GetUserID(string login)
        {
            using (DBContext db = new DBContext())
            {
                return db.Client.First(x => x.Login.ToLower().Equals(login.ToLower())).ID;
            }
        }

        public static string GetUserLogin(Guid userID)
        {
            using (DBContext db = new DBContext())
            {
                return db.Client.First(x => x.ID.Equals(userID)).Login;
            }
        }
    }
}
