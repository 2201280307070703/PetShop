namespace PetShop.Sevices.Data.Contracts
{
    using PetShop.Web.ViewModels.AgeType;
    public interface IAgeTypeService
    {
        Task<IEnumerable<HouseSelectAgeTypeFormModel>> GetAllAgeTypesAsync();

        Task<bool> AgeTypeExistByIdAsync(int id);
    }
}
