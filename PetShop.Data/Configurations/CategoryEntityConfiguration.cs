namespace PetShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetShop.Data.Models;
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            throw new NotImplementedException();
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id=1,
                Name="Food"
            };

            categories.Add(category);

            category = new Category()
            {
                Id = 1,
                Name = "Clothes"
            };

            categories.Add(category);

            category = new Category()
            {
                Id = 1,
                Name = "Beds"
            };

            categories.Add(category);

            category = new Category()
            {
                Id = 1,
                Name = "Toys"
            };

            categories.Add(category);

            category = new Category()
            {
                Id = 1,
                Name = "Dog straps"
            };

            categories.Add(category);

            return categories.ToArray();
        }
    }
}
