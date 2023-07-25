namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Data.Models;
    using PetShop.Services.Data.Models.Product;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.Product;
    using PetShop.Web.ViewModels.Product.Enums;
    using PetShop.Web.ViewModels.Seller;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly PetShopDbContext dbContext;

        public ProductService (PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task BuyProductByIdAsync(string productId, string userId)
        {
           Product product = await this.dbContext.Products.Where(p=>p.IsActive).FirstAsync(p=>p.Id.ToString()==productId);
           ApplicationUser user = await this.dbContext.Users.FirstAsync(u => u.Id.ToString() == userId);
           user.AddedProducts.Add(product);
           product.IsActive = false;

            await this.dbContext.SaveChangesAsync();

        }

        public async Task<string> CreateProductAndReturnIdAsync(ProductFormModel formModel, string sellerId)
        {
            Product product=new Product()
            {
                Name= formModel.Name,
                Description= formModel.Description,
                ImageUrl= formModel.ImageUrl,
                Price= formModel.Price,
                CategoryId= formModel.CategoryId,
                AnimalTypeId= formModel.AnimalTypeId,
                AgeTypeId= formModel.AgeTypeId,
                SellerId=Guid.Parse(sellerId)
            };

            await this.dbContext.Products.AddAsync(product);
            await this.dbContext.SaveChangesAsync();

            return product.Id.ToString();
        }

        public async Task DeleteProductByIdAsync(string productId)
        {
            Product product = await this.dbContext.Products.Where(p=>p.IsActive).FirstAsync(p => p.Id.ToString() == productId);

            product.IsActive = false;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditProductByIdAsync(string productId, ProductFormModel model)
        {
             Product product = await this.dbContext.Products.Where(p => p.IsActive).FirstAsync(p => p.Id.ToString() == productId);

            product.Name = model.Name;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.AgeTypeId = model.AgeTypeId;
            product.AnimalTypeId = model.AnimalTypeId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductAllViewModel>> GetAllProductsBySellerIdAsync(string sellerId)
        {
            return await this.dbContext.Products
                .Where(p => p.IsActive == true && p.SellerId.ToString() == sellerId)
                .Select(s => new ProductAllViewModel
                {
                    Id= s.Id.ToString(),
                    Name= s.Name,
                    Description= s.Description,
                    ImageUrl= s.ImageUrl,
                    Price= s.Price
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductAllViewModel>> GetAllProductsByUserIdAsync(string userId)
        {
            return await this.dbContext.Products
                .Where(p => p.IsActive == true && p.UserId.ToString() == userId)
                .Select(s => new ProductAllViewModel
                {
                    Id = s.Id.ToString(),
                    Name = s.Name,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    Price = s.Price
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductAllViewModel>> GetAllProductsForCurrentAnimalTypeAsync(int animalTypeId)
        {
            return await this.dbContext.Products
                .Where(p=>p.IsActive && p.AnimalTypeId==animalTypeId)
                .Select(s=>new ProductAllViewModel
                {
                    Id = s.Id.ToString(),
                    Name = s.Name,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    Price = s.Price
                })
                .ToListAsync();


        }

        public async Task<IEnumerable<ProductIndexViewModel>> GetLastFiveProductsAsync()
        {
            return await this.dbContext.Products
                .Where(p=>p.IsActive)
                .OrderByDescending(p => p.CreatedOn)
                .Take(5)
                .Select(p => new ProductIndexViewModel
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ImageUrl = p.ImageUrl
                }).ToArrayAsync();
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string productId)
        {
            Product product = await this.dbContext.Products.Where(p => p.IsActive == true)
                .FirstAsync(p => p.Id.ToString() == productId);


            return new ProductDetailsViewModel
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category.Name,
                AnimalType = product.AnimalType.Name,
                AgeType = product.AgeType.TypeOfAge,
                Seller = new SellerDetailsViewModel
                {
                    FirstName = product.Seller.FirstName,
                    LastName = product.Seller.LastName,
                    PhoneNumber = product.Seller.PhoneNumber,
                    Email = product.Seller.Email
                }
            };


        }

        public async Task<ProductPredeleteViewModel> GetProductForDeleteByIdAsync(string id)
        {
            Product product=await this.dbContext.Products.Where(p=>p.IsActive==true)
                .FirstAsync(p=>p.Id.ToString() == id);

            return new ProductPredeleteViewModel
            {
                Name = product.Name,
                ImageUrl= product.ImageUrl,
                Description = product.Description
            };
        }

        public async Task<ProductFormModel> GetProductForEditByIdAsync(string productId)
        {
            Product product=await this.dbContext.Products.Where(p=>p.IsActive)
                .FirstAsync(p=>p.Id.ToString()==productId);

            ProductFormModel model = new ProductFormModel
            {
                Name= product.Name,
                Description= product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                CategoryId= product.CategoryId,
                AgeTypeId= product.AgeTypeId,
                AnimalTypeId= product.AnimalTypeId
            };

            return model;
        }

        public async Task<ProductBuyViewModel> GetProductToBuyByIdAsync(string productId)
        {
            Product product= await this.dbContext.Products.Where(p=>p.IsActive).FirstAsync(p=>p.Id.ToString()==productId);

            return new ProductBuyViewModel
            {
                Name=product.Name,
                ImageUrl= product.ImageUrl,
                Price = product.Price,
                Description= product.Description
            };
        }

        public async Task<bool> ProductExistByIdAsync(string productId)
        {
            return await this.dbContext
                .Products.AnyAsync(p => p.Id.ToString() == productId);
        }

        public async Task<AllProductsFilteredAndPagedServiceModel> SearchProductsAsync(AllProductsQueryModel query)
        {
            IQueryable<Product> productsQuery = dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                productsQuery = productsQuery.Where(p => p.Category.Name == query.Category);
            }

            if (!string.IsNullOrWhiteSpace(query.AnimalType))
            {
                productsQuery=productsQuery.Where(p=>p.AnimalType.Name == query.AnimalType);
            }

            if (!string.IsNullOrWhiteSpace(query.AgeType))
            {
                productsQuery=productsQuery.Where(p=>p.AgeType.TypeOfAge==query.AgeType);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchString))
            {
                string wildCard = $"%{query.SearchString.ToLower()}%";

                productsQuery = productsQuery
                    .Where(p => EF.Functions.Like(p.Name, wildCard) ||
                     EF.Functions.Like(p.Description, wildCard));
            }


            productsQuery = query.ProductSorting switch
            {
                ProductSorting.Newest => productsQuery
                .OrderByDescending(p => p.CreatedOn),
                ProductSorting.Oldest => productsQuery
                .OrderBy(p => p.CreatedOn),
                ProductSorting.PriceAscending => productsQuery
                .OrderBy(p => p.Price),
                ProductSorting.PriceDescending => productsQuery
                .OrderByDescending(p => p.Price),
                _ => productsQuery.OrderBy(p => p.Name)

            };


            IEnumerable<ProductAllViewModel> allProducts = await productsQuery
                .Where(p => p.IsActive)
                .Skip((query.CurrentPage - 1) * query.ProductsPerPage)
                .Take(query.ProductsPerPage)
                .Select(p => new ProductAllViewModel
                {
                    Id=p.Id.ToString(),
                    Name=p.Name,
                    Description=p.Description,
                    Price=p.Price,
                    ImageUrl=p.ImageUrl,
                }).ToArrayAsync();

            int totalProducts=productsQuery.Count();

            return new AllProductsFilteredAndPagedServiceModel()
            {
                TotalProductsCount = totalProducts,
                Products = allProducts
            };
        }

    }
}
 