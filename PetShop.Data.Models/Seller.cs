namespace PetShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static PetShop.Common.EntityValidationConstants.Seller;
    public class Seller
    {
        public Seller()
        {
            this.Products=new HashSet<Product>();
            this.Id=Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }

        public  Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
    }
}
