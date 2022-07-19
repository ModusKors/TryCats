using Xunit;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using DataAccessLayerTests;
using Moq;

namespace DataAccessLayer.Tests
{
    public class UnitOfWorkTests
    {
        [Fact()]
        public async Task CommitTest()
        {
            var mock = new Mock<IUnitOfWork>();

            var result = await mock.Object.Commit();

           Assert.Equal(It.IsAny<int>(), result);
        }
    }
}