using Xunit;
using DataAccessLayer.Finder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Repository;
using DataAccessLayerTests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DataAccessLayer.Finder.Tests
{
    public class CatFinderTests : IDisposable
    {
        DbContextOptions<CatsContext> options = new DbContextOptionsBuilder<CatsContext>().UseInMemoryDatabase(databaseName: "Cats").Options;

        public CatFinderTests()
        {
            Support.SeedData(options);
        }

        public void Dispose()
        {
            Support.CleanUp(options);
        }

        [Fact()]
        public async Task CountTest()
        {
            await using (var context = new CatsContext(options))
            {
                CatFinder catFinder = new CatFinder(context);

                int count = await catFinder.Count();

                Assert.Equal(2, count);
            }
        }

        [Fact()]
        public async Task FindCatByIdTest()
        {
            await using (var context = new CatsContext(options))
            {
                CatFinder catFinder = new CatFinder(context);

                var cat = await catFinder.FindCatById(1);

                Assert.Equal(Support.GenerateCat1().Id, cat.Id);
            }
        }

        [Fact()]
        public async Task FindByNameTest()
        {
            await using (var context = new CatsContext(options))
            {
                CatFinder catFinder = new CatFinder(context);

                var cat = await catFinder.FindByName("Vasya");

                Assert.Equal(Support.GenerateCat1().Name, cat.Name);
            }
        }

        [Fact()]
        public async Task FindAllCatsByNameTest()
        {
            await using (var context = new CatsContext(options))
            {
                CatFinder catFinder = new CatFinder(context);

                var cats = await catFinder.FindAllCatsByName("Vasya");

                Assert.Equal(Support.GenerateCat1().Id, cats[0].Id);
            }
        }
    }
}