using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class LoginRequest
    {
        [DataType(DataType.Text)]
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}