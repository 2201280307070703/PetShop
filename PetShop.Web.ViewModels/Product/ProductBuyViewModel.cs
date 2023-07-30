namespace PetShop.Web.ViewModels.Product
{
    public class ProductBuyViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public string Description { get; set; } = null!;
    }
}
