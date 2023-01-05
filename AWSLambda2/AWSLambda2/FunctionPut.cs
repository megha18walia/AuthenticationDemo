using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambda2
{
    public class FunctionPut
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<Response> FunctionPutHandler(Employee input, ILambdaContext context)
        {
            if (!string.IsNullOrEmpty(input.UserID))
            {
                var client = new AmazonDynamoDBClient();
                var request = new PutItemRequest
                {
                    TableName = "Employee",
                    Item = CreateItemData(input)
                };
                await client.PutItemAsync(request);
                return new Response { Status = System.Net.HttpStatusCode.OK, Message = "User created successfully" };
            }
            else
            {
                return new Response { Status = System.Net.HttpStatusCode.BadRequest, Message = "User Id is mandatory" };
            }
        }

        public static Dictionary<string, AttributeValue> CreateItemData(Employee emp)
        {
            var itemData = new Dictionary<string, AttributeValue>
  {
    { "UserID", new AttributeValue { S = emp.UserID } },
    { "Name", new AttributeValue { S = emp.Name } },
    { "Age", new AttributeValue { N = emp.Age} }
            };

            return itemData;
        }
    }
}
