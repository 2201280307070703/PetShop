namespace PetShop.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data.Models;
    using System.Reflection;

    public class PetShopDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>,Guid>
    {
        public PetShopDbContext(DbContextOptions<PetShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Seller> Sellers { get; set;} = null!;

        public DbSet<AnimalType> AnimalsTypes { get; set; } = null!;

        public DbSet<AgeType> AgeTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(PetShopDbContext)) ?? Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}