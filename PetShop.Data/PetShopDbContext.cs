namespace PetShop.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data.Models;

    public class PetShopDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>,Guid>
    {
        public PetShopDbContext(DbContextOptions<PetShopDbContext> options)
            : base(options)
        {
        }
    }
}