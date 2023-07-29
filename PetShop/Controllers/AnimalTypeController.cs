namespace PetShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    public class AnimalTypeController : BaseController
    {
        [ResponseCache(Duration =60)]
        public IActionResult All()
        {
            return View();
        }
    }
}
