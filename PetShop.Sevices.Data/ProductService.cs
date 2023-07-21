namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Data.Models;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.Product;
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
            Product product = await this.dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.AgeType)
                .Include(p => p.AnimalType)
                .Include(p => p.Seller)
                .Where(p => p.IsActive == true)
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

        public async Task<bool> ProductExistByIdAsync(string productId)
        {
            return await this.dbContext
                .Products.AnyAsync(p => p.Id.ToString() == productId);
        }
    }
}
