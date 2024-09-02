using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.ApplicationEnums;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Domain.ViewModel;
using LocalServicePlatform.Infrastructure.Migrations;
using LocalServicePlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopSpeed.Application.Services.Interface;
using static LocalServicePlatform.Domain.ApplicationEnums.ApplicationEnums;
using TaskersUpdate = LocalServicePlatform.Domain.Models.TaskersUpdate;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using LocalServicePlatform.Application.ApplicationConstants;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

using LocalServicePlatform.Application.ExtensionsMethods;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;




namespace LocalServicePlatform.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class TaskersUpdateController : Controller
    {
        private readonly ITaskerRepository _taskerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserNameService _userName;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<TaskersUpdateController> _logger;
        public TaskersUpdateController(ILogger<TaskersUpdateController> logger, ITaskerRepository taskerRepository, IUnitOfWork unitOfWork, IUserNameService userNameService, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager)
        {
            _taskerRepository = taskerRepository;
            _unitOfWork = unitOfWork;
            _userName = userNameService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _logger = logger;
        }



        [HttpGet]
        public async Task<IActionResult> Taskerdetailshow(string userId)
        {
            try
            {
                // Retrieve the currently logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var taskerupdatedetail = await _unitOfWork.TaskersUpdate.GetByUserId(user.Id);

                    _logger.LogInformation("Number of user taskersUpdate retrieved: {TaskersUpdateCount}", taskerupdatedetail.Count);

                    return View(taskerupdatedetail); // Pass the list directly to the view
                }
                else
                {
                    // Handle the case where the user is not found (e.g., user not logged in)
                    return RedirectToAction("Login", "Account"); // Redirect to login page
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                _logger.LogError(ex, "An error occurred while retrieving taskersUpdate");
                return View("Error");
            }
        }









        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TaskersUpdate> taskersUpdate = await _unitOfWork.TaskersUpdate.GetAllPost();
            var taskersUpdateVM = new TaskersUpdateVM
            {
                TaskersUpdates = taskersUpdate
            };
            return View(taskersUpdateVM);
        }


        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        //fetching records using id 

        {
            TaskersUpdate taskersUpdate = await _unitOfWork.TaskersUpdate.GetPostById(id);

            taskersUpdate.CreatedBy = await _userName.GetUserName(taskersUpdate.CreatedBy);

            taskersUpdate.ModifiedBy = await _userName.GetUserName(taskersUpdate.ModifiedBy);


            return View(taskersUpdate);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Convert Guid to string for comparison
            var userId = user.Id.ToString();
            var existingTaskersUpdate = await _unitOfWork.TaskersUpdate.GetByTaskerIdAsync(userId);

            if (existingTaskersUpdate != null)
            {
                return RedirectToAction("Edit", new { id = existingTaskersUpdate.Id });
            }

            // Create new ViewModel
            IEnumerable<SelectListItem> serviceList = _unitOfWork.Services.Query()
                .Select(x => new SelectListItem
                {
                    Text = x.Name.ToUpper(),
                    Value = x.Id.ToString()
                });

            IEnumerable<SelectListItem> locationList = Enum.GetValues(typeof(Location))
                .Cast<Location>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToString().ToUpper(),
                    Value = ((int)x).ToString()
                });

            TaskersUpdateVM taskersUpdateVM = new TaskersUpdateVM
            {
                TaskersUpdate = new TaskersUpdate(),
                ServiceList = serviceList,
                LocationList = locationList
            };

            return View(taskersUpdateVM);
        }


        [HttpPost]
        public async Task<IActionResult> Create(TaskersUpdateVM taskersUpdateVM)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Handle file upload
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\TaskerPic");
                var extension = Path.GetExtension(file[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(filesStream);
                }

                taskersUpdateVM.TaskersUpdate.ServiceImage = @"\images\TaskerPic\" + newFileName + extension;
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var userId = user.Id.ToString();
                    var existingTaskersUpdate = await _unitOfWork.TaskersUpdate.GetByTaskerIdAsync(userId);

                    if (existingTaskersUpdate == null)
                    {
                        taskersUpdateVM.TaskersUpdate.TaskerId = userId;
                        await _unitOfWork.TaskersUpdate.Create(taskersUpdateVM.TaskersUpdate);
                        await _unitOfWork.SaveAsync();

                        TempData["success"] = CommonMessage.RecordCreated;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["info"] = "You have already created your TaskersUpdate. You can edit it instead.";
                        return RedirectToAction("Edit", new { id = existingTaskersUpdate.Id });
                    }
                }
                return View(taskersUpdateVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a TaskersUpdate");
                return View("Error");
            }
        }



        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var taskersUpdate = await _unitOfWork.TaskersUpdate.GetPostById(id);
            if (taskersUpdate == null)
            {
                return NotFound();
            }

            var serviceList = _unitOfWork.Services.Query()
                .Select(x => new SelectListItem
                {
                    Text = x.Name.ToUpper(),
                    Value = x.Id.ToString()
                });

            var locationList = Enum.GetValues(typeof(Location))
                .Cast<Location>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToString().ToUpper(),
                    Value = ((int)x).ToString()
                });

            var viewModel = new TaskersUpdateVM
            {
                TaskersUpdate = taskersUpdate,
                ServiceList = serviceList,
                LocationList = locationList
            };

            return View(viewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(TaskersUpdateVM taskersUpdateVM)
        {



            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {

                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\TaskerPic");

                var extension = Path.GetExtension(file[0].FileName);



                var objFromDb = await _unitOfWork.TaskersUpdate.GetByIdAsync(taskersUpdateVM.TaskersUpdate.Id);
                if (objFromDb.ServiceImage != null)
                {

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.ServiceImage.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var filesStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(filesStream);
                }

                taskersUpdateVM.TaskersUpdate.ServiceImage = @"\images\TaskerPic\" + newFileName + extension;  //storing path in string brand.BrandLogo

            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.TaskersUpdate.Update(taskersUpdateVM.TaskersUpdate);
                await _unitOfWork.SaveAsync();

                TempData["warning"] = CommonMessage.RecordUpdated;

                return RedirectToAction("Taskerdetailshow");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ShowTaskersToCustomer(TaskersUpdateVM taskersUpdateVM)
        {
            var taskersUpdates = await _unitOfWork.TaskersUpdate.GetAllTaskersUpdates();
            if (taskersUpdates == null || !taskersUpdates.Any())
            {
                return View(new TaskersUpdateVM { TaskersUpdates = new List<TaskersUpdate>() });
            }

            var serviceList = _unitOfWork.Services.Query()
                .Select(x => new SelectListItem
                {
                    Text = x.Name.ToUpper(),
                    Value = x.Id.ToString()
                });

            var locationList = Enum.GetValues(typeof(Location))
                .Cast<Location>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToString().ToUpper(),
                    Value = ((int)x).ToString()
                });

            var viewModel = new TaskersUpdateVM
            {
                TaskersUpdates = taskersUpdates,
                ServiceList = serviceList,
                LocationList = locationList
            };

            return View(viewModel);
        }




        [HttpGet]

        public async Task<IActionResult> Delete(Guid id)
        //fetching records using id 

        {
            TaskersUpdate taskersUpdate = await _unitOfWork.TaskersUpdate.GetByIdAsync(id);

            IEnumerable<SelectListItem> serviceList = _unitOfWork.Services.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });
         

            IEnumerable<SelectListItem> Location = Enum.GetValues(typeof(Location))
               .Cast<Location>()
               .Select(x => new SelectListItem
               {
                   Text = x.ToString().ToUpper(),
                   Value = ((int)x).ToString()
               });


            TaskersUpdateVM taskersUpdateVM = new TaskersUpdateVM
            {
                TaskersUpdate = new TaskersUpdate(),
                ServiceList = serviceList,
              //  ServiceCategoryList = serviceCategoryList,
                LocationList = Location

            };
            return View(taskersUpdateVM);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(TaskersUpdateVM taskersUpdateVM)
        {

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
                var objFromDb = await _unitOfWork.TaskersUpdate.GetByIdAsync(taskersUpdateVM.TaskersUpdate.Id);
                if (objFromDb.ServiceImage != null)
                {

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.ServiceImage.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

            }
            await _unitOfWork.TaskersUpdate.Delete(taskersUpdateVM.TaskersUpdate);
            await _unitOfWork.SaveAsync();

            TempData["error"] = CommonMessage.RecordDeleted;

            return RedirectToAction(nameof(Index));



        }
    }
}