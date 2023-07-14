namespace PetShop.Web.ViewModels.Seller
{
    using System.ComponentModel.DataAnnotations;
    using static PetShop.Common.EntityValidationConstants.Seller;
    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength=FirstNameMinLength)]
        [Display(Name ="First name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength =LastNameMinLength)]
        [Display(Name ="Last name")]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength=PhoneNumberMinLength)]
        [Phone]
        [Display(Name ="Phone")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(EmailMaxLength,MinimumLength =EmailMinLength)]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; } = null!;
    }
}
