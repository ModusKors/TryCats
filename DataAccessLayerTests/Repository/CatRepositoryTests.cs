using Xunit;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Repository;
using DataAccessLayer.Finder;
using DataAccessLayerTests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DataAccessLayer.Repository.Tests
{
    public class CatRepositoryTests : IDisposable
    {
        DbContextOptions<CatsContext> options = new DbContextOptionsBuilder<CatsContext>()
            .UseInMemoryDatabase(databaseName: "Catsdb").Options;

        public CatRepositoryTests()
        {
            Support.SeedData(options);
        }

        public void Dispose()
        {
            Support.CleanUp(options);
        }


        [Fact()]
        public async Task GetAllTest()
        {
            await using (var context = new CatsContext(options))
            {
                CatRepository catRepository = new(context);

                var result = await catRepository.GetAll();

                Assert.Equal(Support.GenerateCats().Count, result.Count);
            }
        }


        [Fact()]
        public async Task GetTest()
        {
            await using (var context = new CatsContext(options))
            {
                CatRepository catRepository = new(context);

                var result = await catRepository.Get(1);

                Assert.Equal(Support.GenerateCat1().Name, result.Name);
            }
        }


        [Fact()]
        public async Task CreateTest()
        {
            await using (var context = new CatsContext(options))
            {
                CatRepository catRepository = new(context);

                catRepository.Create(Support.GenerateCat3());

                var result = await catRepository.GetAll();

                Assert.Equal(3, result.Count);
            }
        }

        [Fact()]
        public void CreateMockTest()
        {
            var mock = new Mock<IRepository<Cat>>();

            var cat = Support.GenerateCat3();

            mock.Object.Create(cat);

            mock.Verify(r => r.Create(cat), Times.Once);
        }


        [Fact()]
        public async Task UpdateTest()
        {
            await using (var context = new CatsContext(options))
            {
                CatRepository catRepository = new(context);

                string name = "Kisi";

                var cat2 = Support.GenerateCat2();
                cat2.Name = name;

                catRepository.Update(cat2);

                var result = await catRepository.Get(2);

                Assert.Equal(name, result.Name);
            }
        }


        [Fact()]
        public async Task DeleteTest()
        {
            await using (var context = new CatsContext(options))
            {
                CatRepository catRepository = new(context);

                catRepository.Delete(Support.GenerateCat2().Id);

                var result = await catRepository.GetAll();

                Assert.Single(result);
            }
        }

        [Fact()]
        public void DeleteMockTest()
        {
            var mock = new Mock<IRepository<Cat>>();

            mock.Object.Delete(Support.GenerateCat2().Id);

            mock.Verify(r => r.Delete(Support.GenerateCat2().Id), Times.Once);
        }

    }
}