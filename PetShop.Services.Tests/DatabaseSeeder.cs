namespace PetShop.Services.Tests
{
    using PetShop.Data;
    using PetShop.Data.Models;

    public class DatabaseSeeder
    {
        public static Category FoodCategory = null!;
        public static Category BedCategory = null!;
        public static Category ToyCategory = null!;

        public static AgeType Junior=null!;

        public static AnimalType Dog=null!;

        public static void SeedDatabase(PetShopDbContext dbContext)
        {
            FoodCategory = new Category()
            {
                Name = "Food"
            };

            BedCategory = new Category()
            {
                Name = "Bed"
            };

            ToyCategory = new Category()
            {
                Name = "Toy"
            };

            dbContext.Categories.Add(FoodCategory);
            dbContext.Categories.Add(BedCategory);
            dbContext.Categories.Add(ToyCategory);


            Junior = new AgeType()
            {
               TypeOfAge="Junior"
            };

            dbContext.AgeTypes.Add(Junior);

            Dog = new AnimalType()
            {
                Name = "Doggo"
            };

            dbContext.AnimalsTypes.Add(Dog);

            dbContext.SaveChanges();

        }

    }
}
