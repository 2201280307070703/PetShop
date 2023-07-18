namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.Category;
    public class CategoryService : ICategoryService
    {
        private readonly PetShopDbContext dbContext;

        public CategoryService(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CategoryExistByIdAsync(int id)
        {
            return await this.dbContext.Categories.AnyAsync(c=>c.Id== id);
        }

        public async Task<IEnumerable<HouseSelectCategoryFormModel>> GetAllCategoriesAsync()
        {
            return await this.dbContext.Categories
                .AsNoTracking()
                .Select(c => new
                HouseSelectCategoryFormModel
                {
                    Id= c.Id,
                    Name = c.Name
                }).ToArrayAsync();
        }
    }
}
