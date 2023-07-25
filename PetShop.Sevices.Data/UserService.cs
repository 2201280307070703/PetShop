namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Data.Models;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.Product;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly PetShopDbContext dbContext;

        public UserService(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductAllViewModel>> GetAllBuyedProductsByIAsync(string userId)
        {
            return await this.dbContext.Products.Where(p => p.UserId.ToString() == userId).Select(p => new ProductAllViewModel
            {
                Name = p.Name,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price
            }).ToArrayAsync();
        }

        public async Task<bool> UserHaveThisProductAlreadyByIdAsync(string userId, string productId)
        {
            Product product = await this.dbContext.Products.FirstAsync(p => p.Id.ToString() == productId);

            return product.UserId.ToString() == userId;
        }
    }
}
