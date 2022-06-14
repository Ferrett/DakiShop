using DakiShop.Models;

namespace DakiShop.Logic
{
    public static class DBService
    {
        public static List<Dakimakura> dakimakuras { get; set; } = new List<Dakimakura>();

        public static void InitDB()
        {
            using (DBContext db = new DBContext())
            {
                db.SaveChanges();
            }
        }

        public static void AddDakimakuraToDB()
        {
            using (DBContext db = new DBContext())
            {
                db.Dakimakura.Add(new Dakimakura
                {
                    Category = db.Category.FirstOrDefault(x=>x.ID==1)!,
                    ImagePath = @"https://dakisource.s3.eu-north-1.amazonaws.com/266947368.jpg",
                    Name = "Капiтан Зеленський",
                    Price = 1000,
                    Size = "170x60",
                    Filler = db.Filler.FirstOrDefault(x => x.ID == 1)!,
                    Manufacturer = db.Manufacturer.FirstOrDefault(x => x.ID == 4)!,
                    Rating = 0
                });
                db.SaveChanges();
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

        public static void GetDakimakurasFromDB()
        {
            using(DBContext db = new DBContext())
            {
                dakimakuras = db.Dakimakura.ToList();
            }
        }
    }
}
