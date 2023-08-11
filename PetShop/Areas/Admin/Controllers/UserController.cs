namespace PetShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetShop.Sevices.Data.Contracts;

    public class UserController : BaseAdminController
    {
        private readonly IAdminService adminService;

        public UserController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        public async Task<IActionResult> All()
        {
            var model = await this.adminService.GetAllUsersAsync();

            return View(model);
        }
    }
}
