using LocalServicePlatform.Application.ApplicationConstants;
using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.ApplicationEnums;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace FinProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = CustomRole.MasterAdmin)]
    public class ServicesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ServicesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)    //injecting constructor
                                                                                                     //handling  the page which has been uploading  when submitting
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            List<Services> services = await _unitOfWork.Services.GetAllAsync();
            return View(services);
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Services services)
        {
            {
                //storing image

                string webRootPath = _webHostEnvironment.WebRootPath; //accessing root path and storing in string
                //accessing uploaded file
                var file = HttpContext.Request.Form.Files; //images comes from request

                if (file.Count > 0)
                { //store only when uploaded only

                    string newFileName = Guid.NewGuid().ToString();//

                    var upload = Path.Combine(webRootPath, @"images\services"); //upload to the specified path

                    var extension = Path.GetExtension(file[0].FileName); //(file[0].FileName)-accessing 1st image in array list or file

                    //copying image by using getting path name and extension  then create file
                    using (var filesStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filesStream); //copying image by we creating a folder  in wwwroot
                    }

                    services.ServicePic = @"\images\services\" + newFileName + extension;  //storing path in string brand.BrandLogo

                }

                if (ModelState.IsValid)
                {

                    await _unitOfWork.Services.Create(services);
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
            Services services = await _unitOfWork.Services.GetByIdAsync(id);
            return View(services);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        //fetching records using id 

        {
            Services services = await _unitOfWork.Services.GetByIdAsync(id); //fetching record which i choose(id) that is equal to db id
            return View(services);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(Services services)
        {
            //suppose if we want to update the img also we shld delete the old one 


            string webRootPath = _webHostEnvironment.WebRootPath; //accessing root path and storing in string
                                                                  //accessing uploaded file
            var file = HttpContext.Request.Form.Files; //images comes from request

            if (file.Count > 0)
            { //store only when uploaded only

                string newFileName = Guid.NewGuid().ToString();//

                var upload = Path.Combine(webRootPath, @"images\services"); //upload to the specified path

                var extension = Path.GetExtension(file[0].FileName); //(file[0].FileName)-accessing 1st image in array list or file


                //delete old immage
                //1st knowing the brand details

                var objFromDb = await _unitOfWork.Services.GetByIdAsync(services.Id);
                if (objFromDb.ServicePic != null)
                {

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.ServicePic.Trim('\\'));
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

                services.ServicePic = @"\images\services\" + newFileName + extension;  //storing path in string brand.BrandLogo

            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.Services.Update(services);
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
            Services services = await _unitOfWork.Services.GetByIdAsync(id);
            return View(services);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Services services)
        {
            //suppose if we want to update the img also we shld delete the old one 


            string webRootPath = _webHostEnvironment.WebRootPath; //accessing root path and storing in string
                                                                  //accessing uploaded file
            var file = HttpContext.Request.Form.Files; //images comes from request

            if (file.Count > 0)
            { //store only when uploaded only

                string newFileName = Guid.NewGuid().ToString();//

                var upload = Path.Combine(webRootPath, @"images\services"); //upload to the specified path

                var extension = Path.GetExtension(file[0].FileName); //(file[0].FileName)-accessing 1st image in array list or file


                //delete old immage
                //1st knowing the brand details

                var objFromDb = await _unitOfWork.Services.GetByIdAsync(services.Id);
                if (objFromDb.ServicePic != null)
                {

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.ServicePic.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

            }
            await _unitOfWork.Services.Delete(services);
            await _unitOfWork.SaveAsync();

            TempData["error"] = CommonMessage.RecordDeleted;

            return RedirectToAction(nameof(Index));
        }

    }
}
