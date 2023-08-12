namespace PetShop.Services.Tests.Tests
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Sevices.Data;
    using PetShop.Sevices.Data.Contracts;
    using static PetShop.Services.Tests.DatabaseSeeder;
    public class CategoryServiceTests
    {
        private DbContextOptions<PetShopDbContext> dbOptions;
        private PetShopDbContext dbContext;

        private ICategoryService categoryService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dbOptions = new DbContextOptionsBuilder<PetShopDbContext>()
                .UseInMemoryDatabase("PetShopInMemory" + Guid.NewGuid().ToString())
                .Options;
            dbContext = new PetShopDbContext(dbOptions, false);

            dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);

            categoryService = new CategoryService(dbContext);
        }

        [Test]
        public async Task CategoryExistByIdAsyncShouldReturnTrueWhenExists()
        {
            int existingCategoryId = FoodCategory.Id;

            bool result = await categoryService.CategoryExistByIdAsync(existingCategoryId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CategoryExistByIdAsyncShouldReturnFalseWhenIdDoNotExists()
        {
            int existingCategoryId = 100;

            bool result = await categoryService.CategoryExistByIdAsync(existingCategoryId);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetAllCategoriesNamesShouldReturnAllNamesAsCollectionOfStrings()
        {
            string[] expected = { "Food", "Clothes", "Beds", "Toys", "Dog straps", "Food", "Bed", "Toy" };

            var result = await categoryService.GetAllCategoryNamesAsync();

            Assert.That(result, Is.EqualTo(expected));
        }

    }
}