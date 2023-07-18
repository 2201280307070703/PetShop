namespace PetShop.Sevices.Data.Contracts
{
    using PetShop.Web.ViewModels.Category;
    public interface ICategoryService
    {
        Task<IEnumerable<HouseSelectCategoryFormModel>> GetAllCategoriesAsync();

        Task<bool> CategoryExistByIdAsync(int id);
    }
}
