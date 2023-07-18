namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.AgeType;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AgeTypeService : IAgeTypeService
    {
        private readonly PetShopDbContext dbContext;

        public AgeTypeService(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AgeTypeExistByIdAsync(int id)
        {
            return await this.dbContext.AgeTypes
                .AnyAsync(at => at.Id == id);
        }

        public async Task<IEnumerable<HouseSelectAgeTypeFormModel>> GetAllAgeTypesAsync()
        {
            return await this.dbContext
                .AgeTypes
                .AsNoTracking()
                .Select(at => new HouseSelectAgeTypeFormModel()
                {
                    Id = at.Id,
                    TypeOfAge=at.TypeOfAge
                }).ToArrayAsync();
        }
    }
}
