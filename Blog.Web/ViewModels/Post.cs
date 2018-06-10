using System;

namespace Blog.ViewModels
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Permalink { get; set; }
        public DateTime Date { get; set; }
        public string Html { get; set; }
    }
}
