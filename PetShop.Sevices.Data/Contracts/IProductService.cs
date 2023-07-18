namespace PetShop.Sevices.Data.Contracts
{
    using PetShop.Web.ViewModels.Product;
    public interface IProductService
    {
        Task<IEnumerable<ProductIndexViewModel>> GetLastFiveProductsAsync();

        Task<string> CreateProductAndReturnIdAsync(ProductFormModel formModel, string sellerId);
    }
}
