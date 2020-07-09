using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PW.Interface;
using PW.Model.RegisterModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PWWebApplication.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IMailService mailService;

        public AccountsController(UserManager<IdentityUser>userManager,
                                    SignInManager<IdentityUser> signInManager,
                                    IMailService mailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailService = mailService;
        }
        //Returns Register page by typing in URL
        [HttpGet]
        public IActionResult Registers()
        {
            return View();
        }
        //Creates new user and if succeeded returned to home page
        [HttpPost]
        public async Task<IActionResult> Registers(Registration registration)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registration.Email,
                    Email = registration.Email
                };
                var result = await userManager.CreateAsync(user, registration.Password);
                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new { userId = user.Id, token = token }, Request.Scheme);
                    await mailService.SendEmailAsync(registration.Email, "Account Confirmation", confirmationLink);
                    return RedirectToAction("Logins", "Accounts");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registration);
        }
        //Confirms email after user clicks the link provided in their email
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId == null || token == null)
            {
                return View("Registers");
            }
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"The user Id {userId} is Invalid";
                return RedirectToPage("/Error");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return RedirectToPage("/Error");
        }
        //logout user
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
        //Returns login url from external login such as google
        [HttpGet]
        public async Task<IActionResult> Logins(string returnUrl)
        {
            Login model = new Login
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }
        //logs in user with our own register form
        [HttpPost]
        public async Task<IActionResult> Logins(Login login, string returnUrl)
        {
            login.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(login.Email);
                if (user != null && !user.EmailConfirmed
                    && (await userManager.CheckPasswordAsync(user, login.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet.");
                    return View(login);
                }
                var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToPage("/Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(login);
        }
        //External login(google) button in layout
        //redirects to google's signin page and then back to our application
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Accounts",
                new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        //Handles authenticated identity returned from google
        public async Task<IActionResult>ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            //checks if returned url is null. If it is then initialized to our application root url
            //otherwise have a value of returned url
            returnUrl = returnUrl ?? Url.Content("~/");
            Login login = new Login
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if(remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Logins", login);
            }
            //Checks external login information provided by external login
            var info = await signInManager.GetExternalLoginInfoAsync();
            if(info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Logins", login);
            }
            //If information is provided by external provider then create new user
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            IdentityUser user = null;
            if(email != null)
            {
                user = await userManager.FindByEmailAsync(email);
                if(user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet.");
                    return View("Logins", login);
                }
            }
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                if(email != null)
                {
                    if (user == null)
                    {
                        user = new IdentityUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                        await userManager.CreateAsync(user);
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new { userId = user.Id, token = token }, Request.Scheme);
                        await mailService.SendEmailAsync(email, "Account Confirmation", confirmationLink);
                        if (user != null && !user.EmailConfirmed)
                        {
                            ModelState.AddModelError(string.Empty, "Email not confirmed yet.");
                            return View("Logins", login);
                        }
                    }
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                ViewBag.ErrorTitile = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please register user";
                return View("Error");
            }
        }
    }
}
