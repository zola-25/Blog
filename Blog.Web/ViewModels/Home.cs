using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.ViewModels
{
    public class Home
    {
        public BlogPost BlogPost { get;set; }
        public ICollection<BlogPostLink> BlogPostLinks { get; set; }

        public bool IsDevelopment {get; set;} 
    }
}
