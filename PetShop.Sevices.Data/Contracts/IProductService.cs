namespace PetShop.Sevices.Data.Contracts
{
    using PetShop.Services.Data.Models.Product;
    using PetShop.Web.ViewModels.Product;
    public interface IProductService
    {
        Task<IEnumerable<ProductIndexViewModel>> GetLastFiveProductsAsync();

        Task<string> CreateProductAndReturnIdAsync(ProductFormModel formModel, string sellerId);

        Task<IEnumerable<ProductAllViewModel>> GetAllProductsByUserIdAsync(string userId);

        Task<IEnumerable<ProductAllViewModel>> GetAllProductsBySellerIdAsync(string sellerId);

        Task<bool> ProductExistByIdAsync(string productId);

        Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string productId);

        Task<IEnumerable<ProductAllViewModel>> GetAllProductsForCurrentAnimalTypeAsync(int animalTypeId);

        Task<ProductPredeleteViewModel> GetProductForDeleteByIdAsync(string id);

        Task DeleteProductByIdAsync(string productId);

        Task<ProductFormModel> GetProductForEditByIdAsync(string productId);

        Task EditProductByIdAsync(string productId, ProductFormModel model);

        Task<AllProductsFilteredAndPagedServiceModel> SearchProductsAsync(AllProductsQueryModel query);
        
        Task<ProductBuyViewModel> GetProductToBuyByIdAsync(string productId);

        Task BuyProductByIdAsync(string productId, string userId);
    }   
}
