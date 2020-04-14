using System.Threading.Tasks;
using Blog.Data.Models;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private SignInManager<BlogAdminUser> _signInManager;
        private RoleManager<BlogAdminRole> _roleManager;
        private UserManager<BlogAdminUser> _userManager;

        public AccountController(UserManager<BlogAdminUser> userManager, RoleManager<BlogAdminRole> roleManager, SignInManager<BlogAdminUser> signInManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl = "")
        {
            var model = new LoginRequest { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmSignOut()
        {
            await _signInManager.SignOutAsync();
            return View("SignOutSuccess");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(LoginRequest loginRequest)
        {

            if (ModelState.IsValid)
            {

                var userByEmail = await _userManager.FindByEmailAsync(loginRequest.Email);

                Microsoft.AspNetCore.Identity.SignInResult result;
                if(userByEmail == null) // if can't find user by email address, assumed they provided their username
                {
                    result = await _signInManager.PasswordSignInAsync(
                        loginRequest.Email,
                        loginRequest.Password,
                        loginRequest.RememberMe,
                        false);                   
                }
                else
                {
                    result = await _signInManager.PasswordSignInAsync(userByEmail, loginRequest.Password, loginRequest.RememberMe, false);
                }


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