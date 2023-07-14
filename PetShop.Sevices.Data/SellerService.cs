namespace PetShop.Sevices.Data
{
    using PetShop.Data;
    using PetShop.Sevices.Data.Contracts;

    public class SellerService:ISellerService
    {
        private readonly PetShopDbContext dbContext;
        public SellerService(PetShopDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }


    }
}
