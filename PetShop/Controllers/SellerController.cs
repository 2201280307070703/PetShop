namespace PetShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.Infrastructure.Extensions;
    using PetShop.Web.ViewModels.Seller;
    using static PetShop.Common.NotificationMessagesConstants;
    public class SellerController : BaseController
    {
        private readonly ISellerService sellerService;

        public SellerController(ISellerService sellerService)
        {
            this.sellerService = sellerService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();

            bool isSeller = await this.sellerService
                .SellerExistsByUserIdAsync(userId!);

            if(isSeller)
            {
                this.TempData[ErrorMessage] = "You are already seller!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeSellerFormModel viewModel)
        {
            string? userId = this.User.GetId();

            bool isSeller = await this.sellerService
                .SellerExistsByUserIdAsync(userId!);

            if (isSeller)
            {
                this.TempData[ErrorMessage] = "You are already seller!";

                return RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken =
                await this.sellerService.SellerExistByPhoneNumberAsync(viewModel.PhoneNumber);

            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(viewModel.PhoneNumber), "This phone number is already taken!");
            }

            bool isEmailTaken=
                await this.sellerService.SellerExistByEmailAsync(viewModel.Email);

            if (isEmailTaken)
            {
                this.ModelState.AddModelError(nameof(viewModel.Email), "This email is already taken!");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                await this.sellerService.CreateSellerAsync(userId!, viewModel);
                this.TempData[SuccessMessage] = "Congratulations! You become seller.";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Search", "Product");
        }
    }
}
