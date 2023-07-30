namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Data.Models;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.Product;
    using PetShop.Web.ViewModels.User;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly PetShopDbContext dbContext;
        private readonly ISellerService sellerService;

        public UserService(PetShopDbContext dbContext
            , ISellerService sellerService)
        {
            this.dbContext = dbContext;
            this.sellerService = sellerService;
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

        public async Task<UserProfileViewModel> GetInformationForUserByIdAsync(string userId)
        {
            UserProfileViewModel userModel= new UserProfileViewModel();

            ApplicationUser user= await this.dbContext.Users.Include(u=>u.Seller).FirstAsync(u=>u.Id.ToString().ToUpper()==userId);

            bool isUserSeller= await this.sellerService.SellerExistsByUserIdAsync(userId);

            if (isUserSeller)
            {
                userModel.FirstName = user.Seller.FirstName;
                userModel.LastName = user.Seller.LastName;
            }
            userModel.Id = user.Id;
            userModel.Email = user.Email;

            return userModel;
        }

        public async Task<bool> UserHaveThisProductAlreadyByIdAsync(string userId, string productId)
        {
            Product product = await this.dbContext.Products.FirstAsync(p => p.Id.ToString() == productId);

            return product.UserId.ToString() == userId;
        }
    }
}
