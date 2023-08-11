namespace PetShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetShop.Areas.Admin.ViewModels;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.Infrastructure.Extensions;

    public class ProductController : BaseAdminController
    {
        private readonly ISellerService sellerService;
        private readonly IUserService userService;
        private readonly IProductService productService;

        public ProductController(ISellerService sellerService, IUserService userService, IProductService productService)
        {
            this.sellerService = sellerService;
            this.userService = userService;
            this.productService = productService;
        }

        public async Task<IActionResult> Mine()
        {
            var sellerId = await this.sellerService.GetSellerIdByUserIdAsync(User.GetId()!);

            var model = new AllAdminProductsViewModel()
            {
                BuyedProducts = await this.userService.GetAllBuyedProductsByIAsync(User.GetId()!),
                AddedProducts = await this.productService.GetAllProductsBySellerIdAsync(sellerId!)
            };

            return View(model);
        }
    }
}
