namespace PetShop.Sevices.Data.Contracts
{
    using PetShop.Web.ViewModels.Product;
    using PetShop.Web.ViewModels.User;
    public interface IUserService
    {
        Task<bool> UserHaveThisProductAlreadyByIdAsync(string userId, string productId);

        Task<IEnumerable<ProductAllViewModel>> GetAllBuyedProductsByIAsync(string userId);

        Task<UserProfileViewModel> GetInformationForUserByIdAsync(string userId);

    }
}
