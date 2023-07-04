namespace PetShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static PetShop.Common.EntityValidationConstants.AnimalType;
    public class AnimalType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
