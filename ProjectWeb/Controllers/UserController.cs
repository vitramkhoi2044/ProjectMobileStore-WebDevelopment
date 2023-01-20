using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using ProjectWeb.Models;
using System.Text.Encodings.Web;
using System.Text;
using ProjectWeb.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserStore<AppUser> _userStore;

        public UserController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserStore<AppUser> userStore)
        {
            _dbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.LastName = userModel.LastName;
                user.FirstName = userModel.FirstName;
                user.Sex = userModel.Sex;
                user.DateOfBirth = userModel.DateOfBirth;
                user.Address = userModel.Address;
                user.Email = userModel.Email;
                user.PhoneNumber = userModel.PhoneNumber;

                await _userStore.SetUserNameAsync(user, userModel.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = "" },
                        protocol: Request.Scheme);


                    ViewBag.EmailConfirmationUrl = callbackUrl;
                    ViewBag.DisplayConfirmAccountLink = true;

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return View("RegisterConfirmation", new { email = userModel.Email, returnUrl = "" });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("~/Home/Index");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect("~/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }
            

            return View();
        }

        public async Task<IActionResult> Logout(string returnUrl = "")
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("~/");
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

    }
}
