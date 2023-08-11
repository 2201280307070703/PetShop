namespace PetShop.Sevices.Data.Contracts
{
    using PetShop.Web.ViewModels.User;
    public interface IAdminService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();
    }
}
