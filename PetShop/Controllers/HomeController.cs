namespace PetShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetShop.Models;
    using PetShop.Sevices.Data.Contracts;
    using System.Diagnostics;
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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}