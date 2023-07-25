namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Data.Models;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.AnimalType;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AnimalTypeService : IAnimalTypeService
    {
        private readonly PetShopDbContext dbContext;

        public AnimalTypeService(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AnimalTypeExistByIdAsync(int id)
        {
            return await this.dbContext.AnimalsTypes
                .AnyAsync(at => at.Id == id);
        }

        public async Task<IEnumerable<string>> GetAllAnimalTypeNamesAsync()
        {
            return await this.dbContext.AnimalsTypes.Select(at => at.Name).ToArrayAsync();
        }

        public async Task<IEnumerable<HouseSelectAnimalTypeFormModel>> GetAllAnimalTypesAsync()
        {
            return await this.dbContext.AnimalsTypes
                .AsNoTracking()
                .Select(at => new HouseSelectAnimalTypeFormModel()
                {
                   Id=at.Id,
                   Name=at.Name
                }).ToArrayAsync();
        }

        public async Task<int> GetAnimalTypeIdByAnimalNameAsync(string animalName)
        {
            AnimalType animalType
                 = await this.dbContext.AnimalsTypes.Where(at => at.Name == animalName).FirstAsync();

            return animalType.Id;

        }
    }
}
