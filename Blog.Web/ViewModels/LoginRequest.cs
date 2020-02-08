using System.ComponentModel.DataAnnotations;

namespace Blog.Web.ViewModels
{
    public class LoginRequest
    {
        [DataType(DataType.Text)]
        [Display(Name = "Username or Email Address")]
        [Required]
        public string UsernameOrEmail { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name ="Remember Me?")]

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}