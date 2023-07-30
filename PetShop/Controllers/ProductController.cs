namespace PetShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Mvc;
    using PetShop.Services.Data.Models.Product;
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
        private readonly IUserService userService;

        public ProductController(IProductService productService, ISellerService sellerService
            , ICategoryService categoryService, IAnimalTypeService animalTypeService, IAgeTypeService ageTypeService, IUserService userService)
        {
            this.productService = productService;
            this.sellerService = sellerService;
            this.categoryService = categoryService;
            this.animalTypeService = animalTypeService;
            this.ageTypeService= ageTypeService;
            this.userService = userService;
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

                    return View(products);
                }
                else
                {
                    this.TempData[ErrorMessage] = "You should be a seller if you want to see your products!";

                    return RedirectToAction("Become", "Seller");
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
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool productExist =
                await this.productService.ProductExistByIdAsync(id);

            if (!productExist)
            {
                this.TempData[ErrorMessage] = "Provided product does not exist!";

                return RedirectToAction("All", "AnimalType");
            }

            try
            {
                var model = await this.productService
                    .GetProductDetailsByIdAsync(id);

                return View(model);

            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                return RedirectToAction("Index", "Home");
            }

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

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool productExist=
                await this.productService.ProductExistByIdAsync(id);

            if (!productExist)
            {
                this.TempData[ErrorMessage] = "This product do not exist!";
                return RedirectToAction("All", "AnimalType");
            }


            bool isSeller = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You should be a seller if you want to delete some of the products!";
                return RedirectToAction("Become", "Seller");
            }

            string? sellerId =await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner =
                await this.sellerService.IsSellerOwnerOfTheProductByIdAsync(sellerId!, id);

            if (!isSellerOwner)
            {
                this.TempData[ErrorMessage] = "You should be a owner if you want to delete this product!";
                return RedirectToAction("All", "AnimalType");
            }

            try
            {
                var model = await this.productService.GetProductForDeleteByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                 return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id,ProductPredeleteViewModel model )
        {
            bool productExist =
               await this.productService.ProductExistByIdAsync(id);

            if (!productExist)
            {
                this.TempData[ErrorMessage] = "This product do not exist!";
                return RedirectToAction("All", "AnimalType");
            }


            bool isSeller = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You should be a seller if you want to delete some of the products!";
                return RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner =
                await this.sellerService.IsSellerOwnerOfTheProductByIdAsync(sellerId!, id);

            if (!isSellerOwner)
            {
                this.TempData[ErrorMessage] = "You should be a owner if you want to delete this product!";
                return RedirectToAction("All", "AnimalType");
            }

            try
            {
                await this.productService.DeleteProductByIdAsync(id);

                this.TempData[WarningMessage] = "You delete this product successfull!";
                return RedirectToAction("Mine", "Product");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool productExist =
              await this.productService.ProductExistByIdAsync(id);

            if (!productExist)
            {
                this.TempData[ErrorMessage] = "This product do not exist!";
                return RedirectToAction("All", "AnimalType");
            }


            bool isSeller = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You should be a seller if you want to edit some of the products!";
                return RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner =
                await this.sellerService.IsSellerOwnerOfTheProductByIdAsync(sellerId!, id);

            if (!isSellerOwner)
            {
                this.TempData[ErrorMessage] = "You should be a owner if you want to edit this product!";
                return RedirectToAction("All", "AnimalType");
            }

            try
            {
                var model=await this.productService.GetProductForEditByIdAsync(id);

                model.Categories = await this.categoryService.GetAllCategoriesAsync();
                model.AnimalTypes = await this.animalTypeService.GetAllAnimalTypesAsync();
                model.AgeTypes = await this.ageTypeService.GetAllAgeTypesAsync();

                return View(model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ProductFormModel formModel)
        {
            bool productExist =
              await this.productService.ProductExistByIdAsync(id);

            if (!productExist)
            {
                this.TempData[ErrorMessage] = "This product do not exist!";
                return RedirectToAction("All", "AnimalType");
            }


            bool isSeller = await this.sellerService.SellerExistsByUserIdAsync(this.User.GetId()!);

            if (!isSeller)
            {
                this.TempData[ErrorMessage] = "You should be a seller if you want to edit some of the products!";
                return RedirectToAction("Become", "Seller");
            }

            string? sellerId = await this.sellerService.GetSellerIdByUserIdAsync(this.User.GetId()!);

            bool isSellerOwner =
                await this.sellerService.IsSellerOwnerOfTheProductByIdAsync(sellerId!, id);

            if (!isSellerOwner)
            {
                this.TempData[ErrorMessage] = "You should be a owner if you want to edit this product!";
                return RedirectToAction("Mine", "Product");
            }

            try
            {
               await this.productService.EditProductByIdAsync(id, formModel);

                this.TempData[SuccessMessage] = "You edit this product successfull!";
                return RedirectToAction("Mine", "Product");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Something went wrong, while trying to update your product! Please try again.");


                formModel.Categories = await this.categoryService.GetAllCategoriesAsync();
                formModel.AnimalTypes = await this.animalTypeService.GetAllAnimalTypesAsync();
                formModel.AgeTypes = await this.ageTypeService.GetAllAgeTypesAsync();
                return View(formModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AllProductsQueryModel queryModel)
        {
            AllProductsFilteredAndPagedServiceModel serviceModel=
                await this.productService.SearchProductsAsync(queryModel);

            queryModel.Products = serviceModel.Products;
            queryModel.TotalProducts = serviceModel.TotalProductsCount;
            queryModel.Categories = await categoryService.GetAllCategoryNamesAsync();
            queryModel.AnimalTypes = await animalTypeService.GetAllAnimalTypeNamesAsync();
            queryModel.AgeTypes = await ageTypeService.GetAllAgeTypeNamesAsync();

            return View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Buy(string id)
        {
            bool productExist =
               await this.productService.ProductExistByIdAsync(id);

            if (!productExist)
            {
                this.TempData[ErrorMessage] = "This product do not exist!";
                return RedirectToAction("All", "AnimalType");
            }

            bool userAlreadyHaveThisProduct = await this.userService.UserHaveThisProductAlreadyByIdAsync(this.User.GetId()!, id);

            if (userAlreadyHaveThisProduct)
            {
                this.TempData[ErrorMessage] = "You already have this product. Please choose something else!";
                return RedirectToAction("Search", "Product");
            }

            try
            {
                var model = await this.productService.GetProductToBuyByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Buy(string id, ProductBuyViewModel model)
        {
            bool productExist =
                await this.productService.ProductExistByIdAsync(id);

            if (!productExist)
            {
                this.TempData[ErrorMessage] = "This product do not exist!";
                return RedirectToAction("All", "AnimalType");
            }

            bool userAlreadyHaveThisProduct = await this.userService.UserHaveThisProductAlreadyByIdAsync(this.User.GetId()!, id);

            if (userAlreadyHaveThisProduct)
            {
                this.TempData[ErrorMessage] = "You already have this product. Please choose something else!";
                return RedirectToAction("Search","Product");
            }

            try
            {
                await this.productService.BuyProductByIdAsync(id, this.User.GetId()!);

                this.TempData[SuccessMessage] = "You buy this product successfully!";

                return RedirectToAction("MyCart", "Product");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CartRemove(string id)
        {
            bool productExist =
               await this.productService.ProductExistByIdAsync(id);

            if (!productExist)
            {
                this.TempData[ErrorMessage] = "This product do not exist!";
                return RedirectToAction("All", "AnimalType");
            }

            bool userAlreadyHaveThisProduct = await this.userService.UserHaveThisProductAlreadyByIdAsync(this.User.GetId()!, id);

            if (!userAlreadyHaveThisProduct)
            {
                this.TempData[ErrorMessage] = "This product is not added to your cart";
                return RedirectToAction("MyCart", "Product");
            }

            try
            {
                await this.productService.RemovingProductFromCardByIdAsync(id, this.User.GetId()!);

                this.TempData[WarningMessage] = "You remove this product from your cart.";

                return RedirectToAction("MyCart", "Product");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet] 
        public async Task<IActionResult> MyCart()
        {
            try
            {
                var model=  await this.userService.GetAllBuyedProductsByIAsync(this.User.GetId()!);
                return View(model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured! Please try again.";

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
