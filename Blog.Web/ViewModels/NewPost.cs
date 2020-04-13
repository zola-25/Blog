using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class NewPost
    {
        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content required")]
        public string Html { get; set; }

        [Required(ErrorMessage = "Date Created required")]
        public DateTime CreationDate { get; set; } = DateTime.Today;

        [Remote("CheckExistingUrlSegment", "Blog", HttpMethod = "GET", ErrorMessage = "Url segment already exists")]
        [Required(ErrorMessage = "Unique url segment required")]
        public string UrlSegment { get; set; }
        
    }
}
