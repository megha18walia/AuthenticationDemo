using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;


namespace AWSLambda2
{
    public class FunctionList
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<List<Employee>> FunctionListHandler(ILambdaContext context)
        {
            List<Employee> empList = new List<Employee>();
            var client = new AmazonDynamoDBClient();
            var request = new ScanRequest
            {
                TableName = "Employee",
            };
            var response = await client.ScanAsync(request);

            foreach (var item in response.Items)
            {
                Employee emp = new Employee();
                foreach(var itemDetail in item)
                {
                    switch(itemDetail.Key)
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
                empList.Add(emp);

            }
            return empList;       
        }
    }
}
