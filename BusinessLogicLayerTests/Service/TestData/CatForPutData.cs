using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entity;

namespace BusinessLogicLayerTests.Service.TestData
{
    public class CatForPutData
    {
        // testdata can be used in several classes
        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { new Cat() { Id=0, Name = null, Summary = null }, false };
                yield return new object[] { Support.GenerateCat1(), true };
                yield return new object[] { Support.GenerateCat2(), true };
            }
        }
    }
}
