using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyTesterWeb.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISendGridEmail _sendGridEmail;
        
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ISendGridEmail sendGridEmail)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sendGridEmail = sendGridEmail;
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            
            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                //Password is incorrect
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginVM);

            }

            //User is not found
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var userEmail = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if(userEmail != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var userName = await _userManager.FindByEmailAsync(registerVM.UserName);
            if (userName != null)
            {
                TempData["Error"] = "This Username is already in use";
                return View(registerVM);
            }

            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.UserName,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);            

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]     
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.EmailAddress);
                if (user == null) return RedirectToAction("ForgotPasswordConfirmation");

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                await _sendGridEmail.SendEmailAsync(model.EmailAddress, "Reset Email Confirmation", "Please reset email by going to this " +
                    "<a href=\"" + callbackurl + "\">link</a>");
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordVM.EmailAddress);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "User is not found");
                    return View();
                }
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordVM.Code, resetPasswordVM.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
            }
            return View(resetPasswordVM);
        }
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
