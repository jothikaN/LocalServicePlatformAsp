using Microsoft.AspNetCore.Mvc;

namespace FinProject.Areas.Customer.Controllers
{
    public class ContactController : Controller
    {
        [Area("Customer")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
