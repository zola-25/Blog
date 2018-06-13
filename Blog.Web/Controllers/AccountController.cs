using System.Threading.Tasks;
using Blog.Data.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {

        private SignInManager<BlogUser> _signInManager;

        public AccountController(SignInManager<BlogUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginRequest { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmLogout()
        {
            await _signInManager.SignOutAsync();
            return View("LogoutSuccess");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                        loginRequest.Username,
                        loginRequest.Password,
                        loginRequest.RememberMe,
                        false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(loginRequest.ReturnUrl)
                        && Url.IsLocalUrl(loginRequest.ReturnUrl))
                    {
                        return Redirect(loginRequest.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login details");
            return View(loginRequest);
        }
    }
}