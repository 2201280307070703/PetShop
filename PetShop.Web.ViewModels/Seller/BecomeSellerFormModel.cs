namespace PetShop.Web.ViewModels.Seller
{
    using System.ComponentModel.DataAnnotations;
    using static PetShop.Common.EntityValidationConstants.Seller;
    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength=FirstNameMinLength,ErrorMessage ="Your name shout be with length between {2} and {1} characters!")]
        [Display(Name ="First name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength =LastNameMinLength,ErrorMessage = "Your name shout be with length between {2} and {1} characters!")]
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
