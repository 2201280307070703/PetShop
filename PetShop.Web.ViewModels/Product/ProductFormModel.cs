namespace PetShop.Web.ViewModels.Product
{
    using PetShop.Web.ViewModels.AgeType;
    using PetShop.Web.ViewModels.AnimalType;
    using PetShop.Web.ViewModels.Category;
    using System.ComponentModel.DataAnnotations;
    using static PetShop.Common.EntityValidationConstants.Product;
    public class ProductFormModel
    {
        public ProductFormModel()
        {
            this.Categories = new HashSet<HouseSelectCategoryFormModel>();
            this.AnimalTypes = new HashSet<HouseSelectAnimalTypeFormModel>();
            this.AgeTypes= new HashSet<HouseSelectAgeTypeFormModel>();
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name="Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal),PriceMinValue, PriceMaxValue )]
        public decimal Price { get; set; }

        [Display(Name="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseSelectCategoryFormModel> Categories { get; set; }

        [Display(Name="Type of Animal")]
        public int AnimalTypeId { get; set; }

        public IEnumerable<HouseSelectAnimalTypeFormModel> AnimalTypes { get; set; }

        [Display(Name="Age of Animal")]
        public int AgeTypeId { get; set; }

        public IEnumerable<HouseSelectAgeTypeFormModel> AgeTypes { get; set; }
    }
}
 