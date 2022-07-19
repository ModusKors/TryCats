using Xunit;
using TryCats6.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Finder;
using BusinessLogicLayer.Repository;
using BusinessLogicLayer.Service;
using DataAccessLayer;
using DataAccessLayer.Finder;
using DataAccessLayer.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TryCats6Tests;
using TryCats6Tests.TestData;

namespace TryCats6.Controllers.Tests
{
    public class CatsControllerTests : IDisposable
    {
        private IService<Cat> _catService;

        private IRepository<Cat> _iRepository;
        private ICatFinder _iCatFinder;
        private IUnitOfWork _iUnitOfWork;

        DbContextOptions<CatsContext> options = new DbContextOptionsBuilder<CatsContext>()
            .UseInMemoryDatabase(databaseName: "CatsDb").Options;

        public CatsControllerTests()
        {
            Support.SeedData(options);

            var context = new CatsContext(options);

            _iRepository = new CatRepository(context);

            _iCatFinder = new CatFinder(context);

            _iUnitOfWork = new UnitOfWork(context);

            _catService = new CatService(_iRepository, _iCatFinder, _iUnitOfWork);
        }

        public void Dispose()
        {
           Support.CleanUp(options);
        }


        [Fact()]
        public async Task GetTest()
        {
            var cats = await _catService.Get();
            Assert.Equal(Support.GenerateCats().Count, cats.Count);
        }

        [Theory]
        [GetCatById]
        public async Task GetTest1(int id, Cat expectedResult)
        {
            var getCat = await _catService.Get(id);
            var result = getCat != null ? getCat : null;

            result?.Id.Should().Be(expectedResult?.Id);
            result?.Name.Should().Be(expectedResult?.Name);
            result?.Summary.Should().Be(expectedResult?.Summary);
        }


        [Theory]
        [GetCatByName]
        public async Task GetTest2(string name, Cat expectedResult)
        {
            var getCat = await _catService.Get(name);
            var result = getCat != null ? getCat : null;

            result?.Id.Should().Be(expectedResult?.Id);
            result?.Name.Should().Be(expectedResult?.Name);
            result?.Summary.Should().Be(expectedResult?.Summary);
        }

        [Theory]
        [PostCat]
        public async Task PostTest(Cat cat, ObjectResult expectedResult)
        {
            ObjectResult result;

            if (cat == null)
            {
                result= new BadRequestObjectResult(cat);
            }
            else
            {
                await _catService.Post(cat);
                result = new OkObjectResult(cat);
            }

            result.Value.Should().Be(expectedResult.Value);
        }

        [Theory]
        [PutCat]
        public async Task PutTest(Cat cat, ObjectResult expectedResult)
        {
            ObjectResult result;

            if (cat == null)
            {
                result = new BadRequestObjectResult(cat);
            }
            else
            {
                result =  await _catService.Put(cat) ? new OkObjectResult(cat) : new NotFoundObjectResult(cat);
            }

            result.Value.Should().Be(expectedResult.Value);
        }

        [Theory]
        [DeleteCat]
        public async Task DeleteTest(int id, StatusCodeResult expectedResult)
        {
            StatusCodeResult result;

            result =  await _catService.Delete(id) ? new OkResult() : new NotFoundResult();

            result.StatusCode.Should().Be(expectedResult.StatusCode);
        }
    }
}