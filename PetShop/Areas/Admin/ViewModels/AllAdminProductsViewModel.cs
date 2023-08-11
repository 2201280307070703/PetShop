namespace PetShop.Areas.Admin.ViewModels
{
    using PetShop.Web.ViewModels.Product;
    public class AllAdminProductsViewModel
    {
        public IEnumerable<ProductAllViewModel> BuyedProducts { get; set; } = null!;

        public IEnumerable<ProductAllViewModel> AddedProducts { get; set; } = null!;
    }
}
