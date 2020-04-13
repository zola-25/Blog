using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.ViewModels
{
    public class SelectPosts
    {
        public List<SelectListItem> BlogPosts {get;set;}

        public int PostId {get;set;}
    }

}
