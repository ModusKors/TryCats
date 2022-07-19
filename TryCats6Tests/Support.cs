using BusinessLogicLayer.Entity;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace TryCats6Tests
{
    public  static class Support
    {
        public static Cat GenerateCat1()
        {
            return new Cat { Id = 1, Name = "Vasya", Summary = "Angry" };
        }

        public static Cat GenerateCat2()
        {
            return new Cat { Id = 2, Name = "Olya", Summary = "Fine" };
        }

        public static Cat GenerateCat3()
        {
            return new Cat { Id = 3, Name = "Oleg", Summary = "Big" };
        }

        public static List<Cat> GenerateCats()
        {
            return new List<Cat>() { GenerateCat1(), GenerateCat2() };
        }

        public static void SeedData(DbContextOptions<CatsContext> options)
        {
            // Insert seed data into the database using one instance of the context
            using (var context = new CatsContext(options))
            {
                context.Cats.Add(GenerateCat1());
                context.Cats.Add(GenerateCat2());
                context.SaveChanges();
            }
        }

        public static void CleanUp(DbContextOptions<CatsContext> options)
        {
            using (var context = new CatsContext(options))
            {
                foreach (var cat in context.Cats)
                    context.Cats.Remove(cat);

                context.SaveChanges();
            }
        }
    }
}
