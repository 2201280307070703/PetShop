namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Data.Models;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.Product;
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
    }
}
