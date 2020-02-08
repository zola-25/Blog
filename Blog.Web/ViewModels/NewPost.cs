using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewModels
{
    public class NewPost
    {
        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content required")]
        public string Html { get; set; }

        [Remote("CheckExistingPermalink", "Blog", HttpMethod = "GET", ErrorMessage = "Permalink already exists")]
        [Required(ErrorMessage = "Permalink required")]
        public string Permalink { get; set; }
        
    }
}
