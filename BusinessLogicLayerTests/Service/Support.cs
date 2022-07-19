using BusinessLogicLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayerTests.Service
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
       
    }
}
