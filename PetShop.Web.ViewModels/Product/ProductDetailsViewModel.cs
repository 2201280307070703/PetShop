namespace PetShop.Web.ViewModels.Product
{
    using PetShop.Web.ViewModels.Seller;
    using System.ComponentModel.DataAnnotations;
    public class ProductDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        [Display(Name = "Type Of Animal")]
        public string AnimalType { get; set; } = null!;

        [Display(Name = "Age")]
        public string AgeType { get; set; } = null!;

        public SellerDetailsViewModel Seller { get; set; } = null!;
    }
}
