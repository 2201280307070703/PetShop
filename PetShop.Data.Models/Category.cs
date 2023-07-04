namespace PetShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static PetShop.Common.EntityValidationConstants.Category;
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
