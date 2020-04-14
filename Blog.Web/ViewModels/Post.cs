using System;

namespace Blog.Web.ViewModels
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Html { get; set; }
        public bool LatestFive { get; set; }

        public string Permalink {get;set;}
        public string Path {get;set;}

    }
}
