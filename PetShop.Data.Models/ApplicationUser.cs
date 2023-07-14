namespace PetShop.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    public class ApplicationUser:IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id=Guid.NewGuid();
            this.AddedProducts=new HashSet<Product>();
        }

        public virtual ICollection<Product> AddedProducts { get; set; }

        public virtual Guid SellerId { get; set; }

        public virtual Seller Seller { get; set; } = null!;
    }
}
