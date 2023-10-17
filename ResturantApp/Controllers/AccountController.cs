using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol;
using RestaurantApp.BL.Services;
using RestaurantApp.BL.VM;
using RestaurantApp.DAL.Extend;
using System.Security.Claims;

namespace ResturantApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signmanager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signmanager,UserManager<ApplicationUser> userManager)
        {
            this._signmanager = signmanager;
            this._userManager = userManager;
        }
        public async Task<IActionResult> Login(string ?ReturnUrl=null)
        {
            LoginVM login = new LoginVM
            {
                ReturnUrl = ReturnUrl,
                ExternalLogin = (await _signmanager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(login);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            try {
                if (ModelState.IsValid)
                {


                    var user = await _userManager.FindByEmailAsync(login.Email);
                    if (user != null)
                    {
                        if (user.isActive)
                        {
                            var result = await _signmanager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.error = "Invlid Email Or Password Attempt ";
                                return View(login);
                            }
                        }
                        else
                        {
                            ViewBag.error = "Your Account Not Active ";
                            return View(login);
                        }
                       
                    }
                    else
                    {
                        ViewBag.error = "Invlid Email Or Password Attempt ";
                        return View(login);
                    }

                }
                return View(login);
            }
            catch(Exception ex) {
                ViewBag.error = "Invlid Email Or Password Attempt ";
                return View(login);
                    
            
            }
      }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = register.UserName,
                        Email = register.Email,
                        isActive = true

                    };

                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(register);
                    }

                }
                return View(register);
            }
            catch(Exception ex)
            {
                return View(register);
            }
        
            
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider)
        {
            var redirect = Url.Action("ExternalLoginCallback", "Account");
            var properities = _signmanager.ConfigureExternalAuthenticationProperties(provider, redirect);
            return new ChallengeResult(provider, properities);
        }

        public async Task<IActionResult> ExternalLoginCallback(string ReturnUrl = null, string remoteError = null)
        {
            ReturnUrl = ReturnUrl ?? Url.Content("~/");
            LoginVM log = new LoginVM
            {
                ReturnUrl = ReturnUrl,
                ExternalLogin = (await _signmanager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError("", $"Error from External Provider:{remoteError}");
                return View("Login", log);
            }
            var info = await _signmanager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError("", "Error Loading Externally");
                return View("Login", log);
            }
            var signinres = await _signmanager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signinres.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var Email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (Email != null)
                {
                    var user = await _userManager.FindByEmailAsync(Email);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            Id = info.Principal.FindFirstValue(ClaimTypes.Email),
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)


                        };
                        await _userManager.CreateAsync(user);
                        await _userManager.AddLoginAsync(user, info);
                    }

                    await _signmanager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.ErrorTitle = $"Email claim do not recieved from:{info.LoginProvider}";
            ViewBag.ErrorMessage = "please contact suuport on atiffahmykhamis@gmail.com";
            return View("Error");
        }


        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task <IActionResult> ForgetPassword(ForgetPasswordVM forgetPassword)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(forgetPassword.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Invalid Email");
                        return View(forgetPassword);
                    }
                    else
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                       
                        var link = Url.Action("ResetPassword", "Account", new { email = forgetPassword.Email, token = token }, Request.Scheme);

                        var x=await MailSender.sendmail(forgetPassword.Email, "Reset Password", link);
                        if (x)
                        {
                            return RedirectToAction("ConfirmForgetPassword", "Account");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Please try Again");
                            return View(forgetPassword);
                        }
                    }
                }
                return View(forgetPassword);
            }catch(Exception e)
            {
                return View(forgetPassword);
            }
          
        }

        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string email,string token)
        {
            if(email!=null && token != null)
            {
                ViewBag.email = email;
                ViewBag.token= token;
                return View();
            }
            else
            {
                return RedirectToAction("Register");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            try
            {

           
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPassword.email);
                if (user != null)
                {
                  var result=  await _userManager.ResetPasswordAsync(user, resetPassword.token, resetPassword.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                       
                    }
                }
            }
            return View();
            }catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        public async Task <IActionResult> Logout()
        {
        await  _signmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
         

    }
}
