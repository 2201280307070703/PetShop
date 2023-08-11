namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminService : IAdminService
    {
        private readonly PetShopDbContext dbContext;

        public AdminService(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
        {
            var users = await this.dbContext.Users
                .Select(u => new AllUsersViewModel()
                {
                    Id=u.Id.ToString(),
                    Email=u.Email
                }).ToListAsync();

           foreach(var user in users)
            {
                var seller = await this.dbContext.Sellers.FirstOrDefaultAsync(s => s.UserId.ToString() == user.Id);

                if(seller != null)
                {
                    user.PhoneNumber = seller.PhoneNumber;

                }
                else
                {
                    user.PhoneNumber=string.Empty;
                }
            }

           return users;
        }
    }
}
