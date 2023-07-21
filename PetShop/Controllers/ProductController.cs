namespace PetShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.Infrastructure.Extensions;
    using PetShop.Web.ViewModels.Product;
    using static PetShop.Common.NotificationMessagesConstants;

    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly ISellerService sellerService;
        private readonly ICategoryService categoryService;
        private readonly IAnimalTypeService animalTypeService;
        private readonly IAgeTypeService ageTypeService;

        public ProductController(IProductService productService, ISellerService sellerService
            , ICategoryService categoryService, IAnimalTypeService animalTypeService, IAgeTypeService ageTypeService)
        {
            this.productService = productService;
            this.sellerService = sellerService;
            this.categoryService = categoryService;
            this.animalTypeService = animalTypeService;
            this.ageTypeService= ageTypeService;
        }

        [HttpGet]
       public async Task<IActionResult> Add()
        {
            bool isSeller = await this.sellerService
                .UserIsSellerByUserIdAsycn(this.User.GetId()!);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You shold be a seller if you want to add products!";
                return RedirectToAction("Become", "Seller");
            }

            try
            {
                ProductFormModel formModel= new ProductFormModel();
                formModel.Categories = await this.categoryService.GetAllCategoriesAsync();
                formModel.AgeTypes=await this.ageTypeService.GetAllAgeTypesAsync();
                formModel.AnimalTypes = await this.animalTypeService.GetAllAnimalTypesAsync();

                return View(formModel);
            }
            catch (Exception)
            {
               return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductFormModel formModel)
        {
            bool isSeller = await this.sellerService
               .UserIsSellerByUserIdAsycn(this.User.GetId()!);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You shold be a seller if you want to add products!";
                return RedirectToAction("Become", "Seller");
            }
            bool categoryExist =
               await this.categoryService.CategoryExistByIdAsync(formModel.CategoryId);

            if (!categoryExist)
            {
                ModelState.AddModelError(nameof(formModel.CategoryId), "This category does not exist. Please try again!");

                formModel.Categories = await this.categoryService.GetAllCategoriesAsync();
                formModel.AnimalTypes = await this.animalTypeService.GetAllAnimalTypesAsync();
                formModel.AgeTypes = await this.ageTypeService.GetAllAgeTypesAsync();
                return View(formModel);
            }

            bool animalTypeExist=
                await this.animalTypeService.AnimalTypeExistByIdAsync(formModel.AnimalTypeId);

            if (!animalTypeExist)
            {
                ModelState.AddModelError(nameof(formModel.AnimalTypeId), "This type of animal does not exist. Please try again!");

                formModel.Categories = await this.categoryService.GetAllCategoriesAsync();
                formModel.AnimalTypes = await this.animalTypeService.GetAllAnimalTypesAsync();
                formModel.AgeTypes = await this.ageTypeService.GetAllAgeTypesAsync();
                return View(formModel);
            }


            bool ageTypeExist =
                await this.ageTypeService.AgeTypeExistByIdAsync(formModel.AgeTypeId);

            if (!ageTypeExist)
            {
                ModelState.AddModelError(nameof(formModel.AgeTypeId), "This type of age does not exist. Please try again!");

                formModel.Categories = await this.categoryService.GetAllCategoriesAsync();
                formModel.AnimalTypes = await this.animalTypeService.GetAllAnimalTypesAsync();
                formModel.AgeTypes = await this.ageTypeService.GetAllAgeTypesAsync();
                return View(formModel);
            }

            if (!this.ModelState.IsValid)
            {
                formModel.Categories = await this.categoryService.GetAllCategoriesAsync();
                formModel.AnimalTypes = await this.animalTypeService.GetAllAnimalTypesAsync();
                formModel.AgeTypes = await this.ageTypeService.GetAllAgeTypesAsync();

                return View(formModel);
            }

            try
            {
                var sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

                var productId = await this.productService.CreateProductAndReturnIdAsync(formModel, sellerId!);

                this.TempData[SuccessMessage] = "Product was added successfully!";

                return this.RedirectToAction("Index", "Home");

            }catch (Exception)
            {
                this.ModelState.AddModelError("", "Unexpected error occured!");

                formModel.Categories = await this.categoryService.GetAllCategoriesAsync();
                formModel.AnimalTypes = await this.animalTypeService.GetAllAnimalTypesAsync();
                formModel.AgeTypes = await this.ageTypeService.GetAllAgeTypesAsync();

                return View(formModel);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<ProductAllViewModel> products = new List<ProductAllViewModel>();

            bool isSeller = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            try
            {
                if (isSeller)
                {
                    string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

                    products.AddRange(await this.productService.GetAllProductsBySellerIdAsync(sellerId!));
                }
                else
                {
                    products.AddRange(await this.productService.GetAllProductsByUserIdAsync(this.User.GetId()!));
                }

                return View(products);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string productId)
        {
            //bool productExist =
            //    await this.productService.ProductExistByIdAsync(productId);

            //if (!productExist)
            //{
            //    this.TempData[ErrorMessage] = "Provided product does not exist!";

            //    return RedirectToAction("All", "Product");
            //}

            //try
            //{
            //    var model =await this.productService
            //        .GetProductDetailsByIdAsync(productId);

            //    return View(model);

            //}catch (Exception)
            //{
            //    this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

            //    return RedirectToAction("Index", "Home");
            //}

            var model = await this.productService
                   .GetProductDetailsByIdAsync(productId);

                return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CatProducts()
        {
            int AnimalTypeId =
                await this.animalTypeService.GetAnimalTypeIdByAnimalNameAsync("Cat");

            var model=await this.productService
                .GetAllProductsForCurrentAnimalTypeAsync(AnimalTypeId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DogProducts()
        {
            int AnimalTypeId =
                await this.animalTypeService.GetAnimalTypeIdByAnimalNameAsync("Dog");

            var model = await this.productService
                .GetAllProductsForCurrentAnimalTypeAsync(AnimalTypeId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> BirdProducts()
        {
            int AnimalTypeId =
                await this.animalTypeService.GetAnimalTypeIdByAnimalNameAsync("Bird");

            var model = await this.productService
                .GetAllProductsForCurrentAnimalTypeAsync(AnimalTypeId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> FishProducts()
        {
            int AnimalTypeId =
                await this.animalTypeService.GetAnimalTypeIdByAnimalNameAsync("Fish");

            var model = await this.productService
                .GetAllProductsForCurrentAnimalTypeAsync(AnimalTypeId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RodentProducts()
        {
            int AnimalTypeId =
                await this.animalTypeService.GetAnimalTypeIdByAnimalNameAsync("Rodent");

            var model = await this.productService
                .GetAllProductsForCurrentAnimalTypeAsync(AnimalTypeId);

            return View(model);
        }
    }
}
