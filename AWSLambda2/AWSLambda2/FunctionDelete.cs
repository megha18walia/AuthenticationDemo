using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;

namespace AWSLambda2
{
    public class FunctionDelete
    {
        public async Task<Response> FunctionDeleteHandler(string UserID, ILambdaContext context)
        {
            var client = new AmazonDynamoDBClient();
            var request = new DeleteItemRequest
            {
                TableName = "Employee",
                Key = new Dictionary<string, AttributeValue>
                  {
                    { "UserID", new AttributeValue {S = UserID } }
                  },
            };
            var response = await client.DeleteItemAsync(request);
            return new Response { Status = response.HttpStatusCode, Message = "User deleted successfully" };

        }
    }
}
