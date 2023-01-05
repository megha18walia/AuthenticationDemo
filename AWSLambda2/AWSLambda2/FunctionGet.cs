using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;

namespace AWSLambda2
{
    public class FunctionGet
    {
        public async Task<Employee> FunctionGetHandler(string UserID, ILambdaContext context)
        {
            Employee emp = new Employee();
            var client = new AmazonDynamoDBClient();
            var request = new GetItemRequest
            {
                TableName = "Employee",
                Key = new Dictionary<string, AttributeValue>
                  {
                    { "UserID", new AttributeValue {S = UserID } }
                  },
            };
            var response = await client.GetItemAsync(request);

                foreach (var itemDetail in response.Item)
                {
                    switch (itemDetail.Key)
                    {
                        case "UserID":
                            emp.UserID = itemDetail.Value.S;
                            break;
                        case "Name":
                            emp.Name = itemDetail.Value.S;
                            break;
                        case "Age":
                            emp.Age = itemDetail.Value.N;
                            break;

                    }



            }
            return emp;
        }
    }
}
