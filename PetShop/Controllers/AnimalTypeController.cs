namespace PetShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    public class AnimalTypeController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
