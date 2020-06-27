using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PW.Model.RegisterModel;
using System.Threading.Tasks;

namespace PWWebApplication.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountsController(UserManager<IdentityUser>userManager,
                                    SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Registers()
        {
            return View();
        }
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
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Index");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registration);
        }
        [HttpPost]
        public async Task<IActionResult> Logouts()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
        [HttpGet]
        public IActionResult Logins()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logins(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                
            }
            return View(login);
        }
    }
}
