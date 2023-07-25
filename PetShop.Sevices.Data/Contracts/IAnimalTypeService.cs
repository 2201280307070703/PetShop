namespace PetShop.Sevices.Data.Contracts
{
    using PetShop.Web.ViewModels.AnimalType;

    public interface IAnimalTypeService
    {
        Task<IEnumerable<HouseSelectAnimalTypeFormModel>> GetAllAnimalTypesAsync();

        Task<bool> AnimalTypeExistByIdAsync(int id);


        Task<int> GetAnimalTypeIdByAnimalNameAsync(string animalName);

        Task<IEnumerable<string>> GetAllAnimalTypeNamesAsync();
    }
}
