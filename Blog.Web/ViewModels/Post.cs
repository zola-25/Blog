using System;

namespace Blog.ViewModels
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Permalink { get; set; }
        public DateTime CreationDate { get; set; }
        public string Html { get; set; }
        public bool LatestFive { get; set; }
    }
}
