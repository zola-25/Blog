using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.ViewModels
{
    public class EditablePost
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content required")]
        public string Html { get; set; }

        [Required(ErrorMessage = "Date Created required")]
        public DateTime CreationDate { get; set; } = DateTime.Today;

        [Remote("CheckExistingUrlSegment", "EditPostForm", AdditionalFields= "PostId", HttpMethod = "GET", ErrorMessage = "URL segment already exists")]
        [Required(ErrorMessage = "Unique URL segment required")]
        public string UrlSegment { get; set; }

        public string Hidden { get; set; }

        public string Path {get;set;}
        public bool NewlyCreated { get; set; }
    }
}
