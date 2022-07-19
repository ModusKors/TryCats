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
    public class DeleteCatAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { 0, new NotFoundResult() };
            yield return new object[] { 1, new OkResult() };
        }
    }
}
