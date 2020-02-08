using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.ViewModels
{
    public class BlogPostLink
    {
        public string Title { get; set; }
        public string Permalink { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
