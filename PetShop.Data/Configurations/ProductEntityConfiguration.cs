namespace PetShop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetShop.Data.Models;
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasData(this.GenerateProducts());

            builder
                 .HasOne(p => p.Category)
                 .WithMany(c => c.Products)
                 .HasForeignKey(p => p.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.AgeType)
                .WithMany(at => at.Products)
                .HasForeignKey(p => p.AgeTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.AnimalType)
                .WithMany(at => at.Products)
                .HasForeignKey(p => p.AnimalTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Seller)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SellerId);

            builder
                .HasOne(p => p.User)
                .WithMany(u => u.AddedProducts)
                .HasForeignKey(p => p.UserId);

            builder
                .Property(c => c.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(a=>a.IsActive)
                .HasDefaultValue(true);

            builder
                .Property(p => p.Price)
                .HasPrecision(18, 2);

          
        }

        private Product[] GenerateProducts()
        {
            ICollection<Product> products = new HashSet<Product>();

            Product product;

            product = new Product()
            {
                Name="Dog sweatshirt",
                Description="This is cozy and warm cloth for you dog.",
                ImageUrl= "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRBjhNm1ffsW7Von6PLqbcd6Syserv-j6pxw&usqp=CAU",
                Price=10.00M,
                CategoryId=2,
                AnimalTypeId=1,
                AgeTypeId=3,
                SellerId= Guid.Parse("F9A6A707-3EE6-4AE8-92EE-7D49F67C9242")
            };

            products.Add(product);

            product = new Product()
            {
                Name = "Fish food",
                Description = "This is the best food for you fishes, it is rich in vitams and minerals.",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRN5RTYv_uPWx8wEV-D8KFyw6iSQ2wmnP-aPA&usqp=CAU",
                Price = 5.00M,
                CategoryId = 1,
                AnimalTypeId = 4,
                AgeTypeId = 2,
                SellerId = Guid.Parse("F9A6A707-3EE6-4AE8-92EE-7D49F67C9242")
            };

            products.Add(product);

            return products.ToArray();
        }
    }
}
