namespace PetShop.Services.Tests.Tests
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Sevices.Data;
    using PetShop.Sevices.Data.Contracts;
    using static PetShop.Services.Tests.DatabaseSeeder;
    public class AgeTypesServiceTests
    {
        private DbContextOptions<PetShopDbContext> dbOptions;
        private PetShopDbContext dbContext;

        private IAgeTypeService ageTypeService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dbOptions = new DbContextOptionsBuilder<PetShopDbContext>()
                .UseInMemoryDatabase("PetShopInMemory" + Guid.NewGuid().ToString())
                .Options;
            dbContext = new PetShopDbContext(dbOptions, false);

            dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);

            ageTypeService = new AgeTypeService(dbContext);
        }

        [Test]
        public async Task AgeTypeExistsByIdAsyncShoudReturnTrue()
        {
            int ageTypeId = Junior.Id;

            bool result = await ageTypeService.AgeTypeExistByIdAsync(ageTypeId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task AgeTypeWhichDoNotExistsShouldReturnFalse()
        {
            int ageTypeId = 100;

            bool result = await ageTypeService.AgeTypeExistByIdAsync(ageTypeId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAllAgeTypesNamesShouldReturnAllNamesAsCollectionOfStrings()
        {
            string[] expected = { "Newborn", "Junior", "Adult", "Junior" };

            var result = await ageTypeService.GetAllAgeTypeNamesAsync();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
