namespace PetShop.Sevices.Data.Contracts
{
    using PetShop.Web.ViewModels.Seller;
    public interface ISellerService
    {
        Task<bool> SellerExistsByUserIdAsync(string userId);

        Task<bool> SellerExistByPhoneNumberAsync(string phoneNumber);

        Task<bool> SellerExistByEmailAsync(string email);

        Task CreateSellerAsync(string userId, BecomeSellerFormModel model);

        Task<string?> GetSellerIdByUserIdAsync(string userId);

        Task<bool> UserIsSellerByUserIdAsycn(string userId);
    }
}
