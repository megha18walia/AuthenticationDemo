using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using AWSLambda2;
using System.Text.Json;
using Newtonsoft.Json;

namespace AWSLambda2.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new FunctionPut();
            var context = new TestLambdaContext();
            string jsonInput = "{ \"ID\" : 1,\"Name\" : \"Megha\"}";
            dynamic emp = JsonConvert.DeserializeObject(jsonInput);
            var upperCase = function.FunctionHandler(emp, context);

            Assert.Equal(1, upperCase.ID);
            Assert.Equal("Megha", upperCase.Name);
        }
    }
}
