﻿using System;

namespace Blog.Data.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string UrlSegment { get; set; }
        public DateTime CreationDate { get; set; }
        public string Html { get; set; }
    }
}
