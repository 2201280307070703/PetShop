namespace PetShop.Controllers
{
    using Griesoft.AspNetCore.ReCaptcha;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetShop.Data.Models;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.Infrastructure.Extensions;
    using PetShop.Web.ViewModels.User;
    using static PetShop.Common.NotificationMessagesConstants;

    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public UserController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
            ,IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
        }

       [HttpGet]
       public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateRecaptcha(Action = nameof(Register),ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if(!ModelState.IsValid) 
            { 
                return View(model);
            }

            ApplicationUser user = new ApplicationUser();

            await userManager.SetEmailAsync(user, model.Email);
            await userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result=
                await userManager.CreateAsync(user, model.Password);

            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model=new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateRecaptcha(Action = nameof(Login), ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                this.TempData[ErrorMessage] = "Something went wrong while logging you! Please try again.";
                return View(model);
            }

            return Redirect(model.ReturnUrl ?? "/Home/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (!this.User.Identity?.IsAuthenticated ?? false)
            {
                this.TempData[ErrorMessage] = "You should Log In if you want to access your profile!";
                return RedirectToAction("Login", "User");
            }

            var model = await this.userService.GetInformationForUserByIdAsync(this.User.GetId()!);

            return View(model);
        }
    }
}
