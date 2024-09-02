using LocalServicePlatform.Application.ApplicationConstants;
using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Application.ExtensionsMethods;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;



namespace FinProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork ,IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var popularPro = await _unitOfWork.PopularPro.GetAllAsync();
            return View(popularPro);
        }











        [HttpGet]
        public async Task<IActionResult> BookNow(int? page, bool resetFilter = false)
        {
            IEnumerable<SelectListItem> serviceList = _unitOfWork.Services.Query().Select(x => new SelectListItem
            {

                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()

            });
            IEnumerable<SelectListItem> serviceCategoryList = _unitOfWork.ServiceCategories.Query().Select(x => new SelectListItem
            {

                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()

            });

            List<Post> posts;


            if (resetFilter)
            {
                TempData.Remove("FilteredPosts");
                TempData.Remove("SelectedServiceId");
                TempData.Remove("SelectedServiceCategoryId");
            }
            if (TempData.ContainsKey("FilteredPosts"))
            {
                posts = TempData.Get<List<Post>>("FilteredPosts");
                TempData.Keep("FilteredPosts");
            }
            else
            {
                posts = await _unitOfWork.Post.GetAllPost();
            }


            int pageSize = 30;

            int pageNumber = page ?? 1;

            int totalItems = posts.Count;

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            var pagedPosts = posts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            HttpContext.Session.SetString("PreviousUrl", HttpContext.Request.Path);

            HomePostVM homePostVM = new HomePostVM
            {
                Posts = pagedPosts,
                ServiceCategoryList = serviceCategoryList,
                ServiceList = serviceList,

                ServiceId = (Guid?)TempData["SelectedServiceId"],
                ServiceCategoryId = (Guid?)TempData["SelectedServiceCategoryId"]

            };

            return View(homePostVM);
        }

        [HttpPost]
        public async Task<IActionResult> BookNow(HomePostVM homePostVM)
        {
            var posts = await _unitOfWork.Post.GetAllPost(homePostVM.searchBox, homePostVM.ServiceId, homePostVM.ServiceCategoryId);

            TempData.Put("FilteredPosts", posts);
            TempData["SelectedServiceId"] = homePostVM.ServiceId;
            TempData["SelectedServiceCategoryId"] = homePostVM.ServiceCategoryId;

            return RedirectToAction("BookNow", new { page = 1, resetFilter = false });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}