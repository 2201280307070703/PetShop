namespace PetShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetShop.Sevices.Data.Contracts;
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.productService.GetLastFiveProductsAsync();

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if(statusCode==404 || statusCode == 400)
            {
                return View("Error404");
            }

            return View();
        }
    }
}