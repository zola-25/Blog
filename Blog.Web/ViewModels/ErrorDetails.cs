using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.ViewModels
{
    public class ErrorDetails
    {
        public int StatusCode {get; set;}
        public string ErrorMessage {get; set;}
        public string RequestId { get; set; }
    }
}
