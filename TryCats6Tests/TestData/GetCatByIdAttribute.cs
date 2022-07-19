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
    public class GetCatByIdAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var getCat = Support.GenerateCat1();
            yield return new object[] { 0, null };
            yield return new object[] { 1, getCat };
        }
    }
}
