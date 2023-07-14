using PetShop.Web.ViewModels.Product;

namespace PetShop.Sevices.Data.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductIndexViewModel>> GetLastFiveProductsAsync();
    }
}
