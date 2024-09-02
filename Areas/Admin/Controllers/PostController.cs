using LocalServicePlatform.Application.ApplicationConstants;
using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.ApplicationEnums;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Domain.ViewModel;
using LocalServicePlatform.Infrastructure.Common;
using LocalServicePlatform.Infrastructure.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Drawing2D;
using TopSpeed.Application.Services.Interface;
using static LocalServicePlatform.Domain.ApplicationEnums.ApplicationEnums;

namespace FinProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
           private readonly ILogger<PostController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserNameService _userName;
        public PostController(ILogger <PostController> logger,IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IUserNameService userName)    //injecting constructor
                                                                                                 //handling  the page which has been uploading  when submitting
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userName = userName;
        }
       [HttpGet]
public async Task<IActionResult> Index()
{
    try
    {
        List<Post> posts = await _unitOfWork.Post.GetAllPost();
        return View(posts);
    }
    catch (Exception ex)
    {
        // Log the exception
        _logger.LogError(ex, "An error occurred while retrieving the posts.");
        TempData["error"] = "An error occurred while retrieving the posts.";
        return View(new List<Post>());
    }
}

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> serviceList = _unitOfWork.Services.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });
            IEnumerable<SelectListItem> serviceCategoryList = _unitOfWork.ServiceCategories.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper()  ,
                Value = x.Id.ToString()
            });
            IEnumerable<SelectListItem> serviceType = Enum.GetValues(typeof(ServiceType))
        .Cast<ServiceType>()
        .Select(x => new SelectListItem
        {

            Text = x.ToString().ToUpper(),
            Value = ((int)x).ToString()
        });

            PostVM postVM = new PostVM
            {
                Post = new Post(),
                ServiceList = serviceList,
                ServiceCategoryList = serviceCategoryList,
                ServiceTypeList = serviceType

            };
            return View(postVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostVM postVM)
        {
            {
                //storing image

                string webRootPath = _webHostEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files; 

                if (file.Count > 0)
                { 

                    string newFileName = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webRootPath, @"images\post"); 

                    var extension = Path.GetExtension(file[0].FileName);

                   
                    using (var filesStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filesStream); 
                    }

                    postVM.Post.ServiceImage = @"\images\post\" + newFileName + extension; 

                }

                if (ModelState.IsValid)
                {

                    await _unitOfWork.Post.Create(postVM.Post);
                    await _unitOfWork.SaveAsync();


                    TempData["success"] = CommonMessage.RecordCreated;

                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        //to see details of products(brand )
        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        //fetching records using id 

        {
            Post post = await _unitOfWork.Post.GetPostById(id);

           post.CreatedBy=await _userName.GetUserName(post.CreatedBy);

            post.ModifiedBy=await _userName.GetUserName(post.ModifiedBy);


            return View(post);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        //fetching records using id 

        {
            Post post = await _unitOfWork.Post.GetPostById(id);
            //fetching record which i choose(id) that is equal to db id

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
            IEnumerable<SelectListItem> serviceType = Enum.GetValues(typeof(ServiceType))
        .Cast<ServiceType>()
        .Select(x => new SelectListItem
        {

            Text = x.ToString().ToUpper(),
            Value = ((int)x).ToString()
        });

            PostVM postVM = new PostVM
            {
                Post = post,
                ServiceList = serviceList,
                ServiceCategoryList = serviceCategoryList,
                ServiceTypeList = serviceType

            };
            return View(postVM);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(PostVM postVM)
        {
            //suppose if we want to update the img also we shld delete the old one 


            string webRootPath = _webHostEnvironment.WebRootPath; //accessing root path and storing in string
                                                                  //accessing uploaded file
            var file = HttpContext.Request.Form.Files; //images comes from request

            if (file.Count > 0)
            { //store only when uploaded only

                string newFileName = Guid.NewGuid().ToString();//

                var upload = Path.Combine(webRootPath, @"images\post"); //upload to the specified path

                var extension = Path.GetExtension(file[0].FileName); //(file[0].FileName)-accessing 1st image in array list or file


                //delete old immage
                //1st knowing the brand details

                var objFromDb = await _unitOfWork.Post.GetByIdAsync(postVM.Post.Id);
                if (objFromDb.ServiceImage != null)
                {

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.ServiceImage.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                //copying the new image by using getting path name and extension  then create file
                using (var filesStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(filesStream); //copying image by we creating a folder  in wwwroot
                }

                postVM.Post.ServiceImage = @"\images\post\" + newFileName + extension;  //storing path in string brand.BrandLogo

            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.Post.Update(postVM.Post);
                await _unitOfWork.SaveAsync();

                TempData["warning"] = CommonMessage.RecordUpdated;

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Delete(Guid id)
        //fetching records using id 

        {
            Post post = await _unitOfWork.Post.GetByIdAsync(id);

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
            IEnumerable<SelectListItem> serviceType = Enum.GetValues(typeof(ServiceType))
        .Cast<ServiceType>()
        .Select(x => new SelectListItem
        {

            Text = x.ToString().ToUpper(),
            Value = ((int)x).ToString()
        });

            PostVM postVM = new PostVM
            {
                Post = post,
                ServiceList = serviceList,
                ServiceCategoryList = serviceCategoryList,
                ServiceTypeList = serviceType

            };

            return View(postVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(PostVM postVM)
        {
            //suppose if we want to update the img also we shld delete the old one 


            string webRootPath = _webHostEnvironment.WebRootPath; //accessing root path and storing in string
                                                                  //accessing uploaded file
            var file = HttpContext.Request.Form.Files; //images comes from request

            if (file.Count > 0)
            { //store only when uploaded only

                string newFileName = Guid.NewGuid().ToString();//

                var upload = Path.Combine(webRootPath, @"images\post"); //upload to the specified path

                var extension = Path.GetExtension(file[0].FileName); //(file[0].FileName)-accessing 1st image in array list or file


                //delete old immage
                //1st knowing the brand details

                var objFromDb = await _unitOfWork.Post.GetByIdAsync(postVM.Post.Id);
                if (objFromDb.ServiceImage != null)
                {

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.ServiceImage.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

            }
            await _unitOfWork.Post.Delete(postVM.Post);
            await _unitOfWork.SaveAsync();

            TempData["error"] = CommonMessage.RecordDeleted;

            return RedirectToAction(nameof(Index));
        }

    }
}