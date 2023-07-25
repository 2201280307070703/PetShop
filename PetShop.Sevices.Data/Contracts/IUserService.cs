using PetShop.Web.ViewModels.Product;

namespace PetShop.Sevices.Data.Contracts
{
    public interface IUserService
    {
        Task<bool> UserHaveThisProductAlreadyByIdAsync(string userId, string productId);

        Task<IEnumerable<ProductAllViewModel>> GetAllBuyedProductsByIAsync(string userId);

    }
}
