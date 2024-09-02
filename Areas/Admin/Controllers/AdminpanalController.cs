using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LocalServicePlatform.Application.Contracts.Presistance;

namespace FinProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminpanalController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminpanalController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customerCount = await _userRepository.GetCustomerCountAsync();
            var taskerCount = await _userRepository.GetTaskerCountAsync();

            ViewData["CustomerCount"] = customerCount;
            ViewData["TaskerCount"] = taskerCount;

            return View();
        }
    }
}
