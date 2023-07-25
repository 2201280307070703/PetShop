namespace PetShop.Services.Data.Models.Product
{
    using PetShop.Web.ViewModels.Product;
    public class AllProductsFilteredAndPagedServiceModel
    {
        public AllProductsFilteredAndPagedServiceModel() 
        { 
            Products=new HashSet<ProductAllViewModel>();
        }

        public int TotalProductsCount { get; set; }

        public IEnumerable<ProductAllViewModel> Products { get; set; }
    }
}
