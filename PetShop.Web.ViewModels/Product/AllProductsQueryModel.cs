namespace PetShop.Web.ViewModels.Product
{
    using PetShop.Web.ViewModels.Product.Enums;
    public class AllProductsQueryModel
    {
        public AllProductsQueryModel() 
        {
            CurrentPage = 1;
            ProductsPerPage = 1;

            Categories=new HashSet<string>();   
            AnimalTypes=new HashSet<string>();
            AgeTypes=new HashSet<string>();

            Products = new HashSet<ProductAllViewModel>();
        }

        public string? Category { get; set; }

        public string? AnimalType { get; set;}

        public string? AgeType { get; set;}

        public string? SearchString { get; set; }

        public ProductSorting ProductSorting { get; set; }

        public int CurrentPage { get; set; }

        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> AnimalTypes { get; set; }

        public IEnumerable<string> AgeTypes { get; set; }

        public IEnumerable<ProductAllViewModel> Products { get; set; }

    }
}
