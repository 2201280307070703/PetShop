namespace PetShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetShop.Data.Models;
    public class AgeTypeEntityConfiguration : IEntityTypeConfiguration<AgeType>
    {
        public void Configure(EntityTypeBuilder<AgeType> builder)
        {
            builder.HasData(this.GenerateAgeTypes());
        }
        private AgeType[] GenerateAgeTypes()
        {
            ICollection<AgeType> ageTypes = new HashSet<AgeType>();

            AgeType agetype;

            agetype = new AgeType()
            {
                Id = 1,
                TypeOfAge = "Newborn"
            };

            ageTypes.Add(agetype);

            agetype = new AgeType()
            {
                Id = 2,
                TypeOfAge="Junior"
            };

            ageTypes.Add(agetype);

            agetype = new AgeType()
            {
                Id=3,
                TypeOfAge="Adult"
            };

            ageTypes.Add(agetype);

            return ageTypes.ToArray();
        }
    }
}
