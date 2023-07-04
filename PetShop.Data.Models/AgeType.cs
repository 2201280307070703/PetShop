namespace PetShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static PetShop.Common.EntityValidationConstants.AgeType;
    public class AgeType
    {
        public AgeType()
        {
            this.Products=new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AgeTypeMaxLength)]
        public string TypeOfAge { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
