using Microsoft.EntityFrameworkCore.Metadata;
using PetShop.Web.ViewModels.Seller;

namespace PetShop.Sevices.Data.Contracts
{
    public interface ISellerService
    {
        Task<bool> SellerExistsByUserIdAsync(string userId);

        Task<bool> SellerExistByPhoneNumberAsync(string phoneNumber);

        Task<bool> SellerExistByEmailAsync(string email);

        Task CreateSellerAsync(string userId, BecomeSellerFormModel model);
    }
}
