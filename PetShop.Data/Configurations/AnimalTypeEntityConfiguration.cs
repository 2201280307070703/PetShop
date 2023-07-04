namespace PetShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetShop.Data.Models;
    public class AnimalTypeEntityConfiguration : IEntityTypeConfiguration<AnimalType>
    {
        public void Configure(EntityTypeBuilder<AnimalType> builder)
        {
            builder.HasData(this.GenerateAnimalTypes());
        }
        private AnimalType[] GenerateAnimalTypes()
        {
            ICollection<AnimalType> animalTypes=new HashSet<AnimalType>();

            AnimalType animalType;

            animalType = new AnimalType()
            {
                Id=1,
                Name="Dog"
            };

            animalTypes.Add(animalType);

            animalType = new AnimalType()
            {
                Id = 2,
                Name = "Cat"
            };

            animalTypes.Add(animalType);

            animalType = new AnimalType()
            {
                Id = 3,
                Name = "Bird"
            };

            animalTypes.Add(animalType);

            animalType = new AnimalType()
            {
                Id = 4,
                Name = "Fish"
            };

            animalTypes.Add(animalType);

            animalType = new AnimalType()
            {
                Id = 5,
                Name = "Rodent"
            };

            animalTypes.Add(animalType);

            return animalTypes.ToArray();
        }
    }
}
