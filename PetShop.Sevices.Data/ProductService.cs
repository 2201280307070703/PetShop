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
    using static PetShop.Common.EntityValidationConstants.Product;

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
                    Id= s.Id,
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
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    Price = s.Price
                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductIndexViewModel>> GetLastFiveProductsAsync()
        {
            return await this.dbContext.Products
                .Where(p=>p.IsActive)
                .OrderByDescending(p => p.CreatedOn)
                .Take(5)
                .Select(p => new ProductIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl
                }).ToArrayAsync();
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string productId)
        {
            return await this.dbContext.Products
                .Where(p => p.IsActive==true && p.Id.ToString() == productId)
                .Select(p => new ProductDetailsViewModel()
                {
                    Id= p.Id,
                    Name = p.Name,
                    ImageUrl= p.ImageUrl,
                    Price= p.Price,
                    Description = p.Description,
                    Category=p.Category.Name,
                    AnimalType=p.AnimalType.Name,
                    AgeType=p.AgeType.TypeOfAge,
                    Seller=new SellerDetailsViewModel
                    {
                        FirstName=p.Seller.FirstName,
                        LastName=p.Seller.LastName,
                        PhoneNumber=p.Seller.PhoneNumber,
                        Email=p.Seller.Email
                    }}).FirstAsync();
        }

        public async Task<bool> ProductExistByIdAsync(string productId)
        {
            return await this.dbContext
                .Products.AnyAsync(p => p.Id.ToString() == productId);
        }
    }
}
