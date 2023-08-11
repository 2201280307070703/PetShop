namespace PetShop.Sevices.Data
{
    using PetShop.Data;
    using PetShop.Sevices.Data.Contracts;
    public class AdminService : IAdminService
    {
        private readonly PetShopDbContext dbContext;

        public AdminService(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
       
    }
}
