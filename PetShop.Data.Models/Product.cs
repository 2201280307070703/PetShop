namespace PetShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static PetShop.Common.EntityValidationConstants.Product;
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public int AnimalTypeId { get; set; }

        public virtual AnimalType AnimalType { get; set; } = null!;

        public int AgeTypeId { get; set; }

        public virtual AgeType AgeType { get; set; } = null!;

        public Guid SellerId { get; set; }

        public virtual Seller Seller { get; set; } = null!;

        public Guid? UserId { get; set; }

        public ApplicationUser? User { get; set; } 
    }
}
