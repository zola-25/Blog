using System;

namespace Blog.Web.ViewModels
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string UrlSegment { get; set; }
        public DateTime CreationDate { get; set; }
        public string Html { get; set; }
        public bool LatestFive { get; set; }

        public string FullLink {get;set;}
        public string FullLinkEncoded {get;set;}

    }
}
