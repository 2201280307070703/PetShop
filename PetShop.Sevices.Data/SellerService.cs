﻿namespace PetShop.Sevices.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetShop.Data;
    using PetShop.Data.Models;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.Seller;
    using System.Threading.Tasks;

    public class SellerService:ISellerService
    {
        private readonly PetShopDbContext dbContext;
        public SellerService(PetShopDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }

        public async Task CreateSellerAsync(string userId, BecomeSellerFormModel model)
        {
            Seller seller = new Seller()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserId=Guid.Parse(userId)
            };

            await this.dbContext.Sellers.AddAsync(seller);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> SellerExistByEmailAsync(string email)
        {
            return
                await this.dbContext.Sellers
                .AnyAsync(s => s.Email == email);
        }

        public async Task<bool> SellerExistByPhoneNumberAsync(string phoneNumber)
        {
            return
                await this.dbContext.Sellers
                .AnyAsync(s => s.PhoneNumber == phoneNumber);
        }

        public async Task<bool> SellerExistsByUserIdAsync(string userId)
        {
            return
                await dbContext.Sellers
                .AnyAsync(s => s.UserId.ToString() == userId);
        }
    }
}
