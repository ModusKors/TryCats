using Xunit;
using BusinessLogicLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entity;
using BusinessLogicLayer.Finder;
using BusinessLogicLayer.Repository;
using BusinessLogicLayerTests.Service;
using BusinessLogicLayerTests.Service.TestData;
using Castle.DynamicProxy.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;


using FluentAssertions;

namespace BusinessLogicLayer.Service.Tests
{
    public class CatServiceTests
    {
        private Mock<IRepository<Cat>> _iRepository;
        private Mock<ICatFinder> _iCatFinder;
        private Mock<IUnitOfWork> _unitOfWork;

        public CatServiceTests()
        {
            _iRepository = new Mock<IRepository<Cat>>();
            _iCatFinder = new Mock<ICatFinder>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact()]
        public async Task GetTest()
        {
            _iRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult(Support.GenerateCats()));

            var result = await _iRepository.Object.GetAll();
            Assert.NotEmpty(result);
        }

        [Fact()]
        public async Task CountTest()
        {
            _iCatFinder.Setup(repo => repo.Count()).Returns(Task.FromResult(Support.GenerateCats().Count()));

            var result = await _iCatFinder.Object.Count();
            Assert.Equal(Support.GenerateCats().Count(),result);
        }

        [Fact()]
        public async Task GetByIdTest()
        {
            await _iRepository.Object.Get(1);
            _iRepository.Verify(r => r.Get(1), Times.Once);
        }

        [Fact()]
        public async Task GetByNameTest()
        {
            string name = "Vasya";
            await _iCatFinder.Object.FindByName(name);
            _iCatFinder.Verify(r => r.FindByName(name), Times.Once);
        }

        [Fact()]
        public async Task PostTest()
        {
            Cat cat = new Cat();
            _iRepository.Object.Create(cat);
            await _unitOfWork.Object.Commit();

            _iRepository.Verify(r => r.Create(cat), Times.Once);
            _unitOfWork.Verify(r => r.Commit(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CatForPutData.TestData), MemberType = typeof(CatForPutData))]
        public async Task PutTest(Cat cat, bool expectedResult)
        {
            bool? result = null;

            Cat nullCat = null;

            _iRepository.Setup(repo => repo.Get(0)).Returns(Task.FromResult(nullCat));
            _iRepository.Setup(repo => repo.Get(1)).Returns(Task.FromResult(Support.GenerateCat1()));
            _iRepository.Setup(repo => repo.Get(2)).Returns(Task.FromResult(Support.GenerateCat2()));

            var getCat = await _iRepository.Object.Get(cat.Id);
            if (getCat != null)
            {
                _iRepository.Object.Update(cat);
                await _unitOfWork.Object.Commit();
                result =  true;
            }
            else
            {
                result = false;
            }

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(0,false)]
        [InlineData(1,true)]
        public async Task DeleteTest(int id, bool expectedResult)
        {
            Cat nullCat = null;
            _iRepository.Setup(repo => repo.Get(0)).Returns(Task.FromResult(nullCat));
            _iRepository.Setup(repo => repo.Get(1)).Returns(Task.FromResult(Support.GenerateCat1()));

            bool? result = null;

            Cat cat = await _iRepository.Object.Get(id);
            if (cat == null)
            {
                result = false;

            }
            else
            {
                _iRepository.Object.Delete(id);
                await _unitOfWork.Object.Commit();
                result = true;
            }

            result.Should().Be(expectedResult);

        }
    }
    
}