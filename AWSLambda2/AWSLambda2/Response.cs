using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AWSLambda2
{
    public class Response
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
