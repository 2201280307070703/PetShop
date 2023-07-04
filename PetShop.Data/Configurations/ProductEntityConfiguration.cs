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

            builder
                .Property(q => q.Quanity)
                .HasPrecision(18, 2);
        }

        //private Product[] GenerateProducts()
        //{
        //    ICollection<Product> products = new HashSet<Product>();

        //    Product product;

        //    product = new Product()
        //    {
        //        Name = "",
        //        Description = "",
        //        ImageUrl = "",
        //        Price =,
        //        Quanity =,
        //        CategoryId =,
        //        AnimalTypeId =,
        //        AgeTypeId =,
        //        SellerId =,
        //        UserId =
        //    };
        //}
    }
}
