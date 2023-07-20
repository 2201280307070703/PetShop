namespace PetShop.Web.ViewModels.Seller
{
    using System.ComponentModel.DataAnnotations;
    public class SellerDetailsViewModel
    {
        [Display(Name ="First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;
    }
}
