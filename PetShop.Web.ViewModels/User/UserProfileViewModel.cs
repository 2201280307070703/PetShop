namespace PetShop.Web.ViewModels.User
{
    public class UserProfileViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = null!;

        public string? FirstName { get; set; } 

        public string? LastName { get; set; }

    }
}
