namespace PetShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static PetShop.Common.GeneralApplicationConstants;

    [Area(AdminAreaName)]
    [Authorize(Roles =AdminRoleName)]
    public class BaseAdminController : Controller
    {
        
    }
}
