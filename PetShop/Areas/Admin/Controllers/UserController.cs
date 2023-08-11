namespace PetShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using PetShop.Sevices.Data.Contracts;
    using PetShop.Web.ViewModels.User;
    using static PetShop.Common.GeneralApplicationConstants;

    public class UserController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly IMemoryCache memoryCache;

        public UserController(IAdminService adminService, IMemoryCache memoryCache)
        {
            this.adminService = adminService;
            this.memoryCache = memoryCache;
        }
        public async Task<IActionResult> All()
        {
            IEnumerable<AllUsersViewModel> users = this.memoryCache.Get<IEnumerable<AllUsersViewModel>>(UsersCacheKey);

            if(users == null)
            {
                users = await this.adminService.GetAllUsersAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(UsersCacheDucationMinutes));

                this.memoryCache.Set(UsersCacheKey, users, cacheOptions);
            }

            return View(users);
        }
    }
}
