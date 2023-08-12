namespace PetShop.Services.Tests.Tests
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Sevices.Data;
    using PetShop.Sevices.Data.Contracts;
    using static PetShop.Services.Tests.DatabaseSeeder;
    public class AnimalTypesServiceTests
    {
        private DbContextOptions<PetShopDbContext> dbOptions;
        private PetShopDbContext dbContext;

        private IAnimalTypeService animalTypeService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            dbOptions = new DbContextOptionsBuilder<PetShopDbContext>()
                .UseInMemoryDatabase("PetShopInMemory" + Guid.NewGuid().ToString())
                .Options;
            dbContext = new PetShopDbContext(dbOptions, false);

            dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);

            animalTypeService = new AnimalTypeService(dbContext);
        }

        [Test]
        public async Task AnimalTypeExistsByIdShouldReturnTrueWhenIdExists()
        {
            int animalTypeId = Dog.Id;

            bool result = await animalTypeService.AnimalTypeExistByIdAsync(animalTypeId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task AnimalTypeExistsByIdShouldReturnFalseWhenIdDoNotExist()
        {
            int animalTypeId = 100;

            bool result = await animalTypeService.AnimalTypeExistByIdAsync(animalTypeId);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetAnimalTypeIdByAnimalNameAsyncShouldReturnId()
        {
            var expected = Dog.Id;

            var result = await animalTypeService.GetAnimalTypeIdByAnimalNameAsync("Doggo");

            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAllAnimalTypesNamesShouldReturnAllNamesAsCollectionOfStrings()
        {
            string[] expected = { "Dog", "Cat", "Bird", "Fish", "Rodent", "Doggo" };

            var result = await animalTypeService.GetAllAnimalTypeNamesAsync();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
