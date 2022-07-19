using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Xunit.Sdk;

namespace TryCats6Tests.TestData
{
    public class PostCatAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            Cat nullCat = null; 
            var cat = Support.GenerateCat3();
            yield return new object[] { nullCat, new BadRequestObjectResult(nullCat) };
            yield return new object[] { cat, new OkObjectResult(cat) };
        }
    }
}
