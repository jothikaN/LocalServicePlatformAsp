using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.ApplicationEnums;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using LocalServicePlatform.Application.ApplicationConstants;

namespace FinProject.Areas.Admin.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = CustomRole.MasterAdmin)]
    public class PopularProController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PopularProController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PopularPro popularPro)
        {
            if (ModelState.IsValid)
            {
                await SaveImageAndSetPath(popularPro);
                await _unitOfWork.PopularPro.Create(popularPro);
                await _unitOfWork.SaveAsync();
                TempData["success"] = CommonMessage.RecordCreated;
                return RedirectToAction(nameof(Index));
            }
            return View(popularPro);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var popularPro = await _unitOfWork.PopularPro.GetByIdAsync(id);
            return View(popularPro);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var popularPro = await _unitOfWork.PopularPro.GetByIdAsync(id);
            return View(popularPro);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PopularPro popularPro)
        {
            if (ModelState.IsValid)
            {
                await SaveImageAndSetPath(popularPro);
                await _unitOfWork.PopularPro.Update(popularPro);
                await _unitOfWork.SaveAsync();
                TempData["warning"] = CommonMessage.RecordUpdated;
                return RedirectToAction(nameof(Index));
            }
            return View(popularPro);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(Guid id)
        //fetching records using id 

        {
            PopularPro popularpro = await _unitOfWork.PopularPro.GetByIdAsync(id);
            return View(popularpro);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(PopularPro popularpro)
        {
            //suppose if we want to update the img also we shld delete the old one 


            string webRootPath = _webHostEnvironment.WebRootPath; //accessing root path and storing in string
                                                                  //accessing uploaded file
            var file = HttpContext.Request.Form.Files; //images comes from request

            if (file.Count > 0)
            { //store only when uploaded only

                string newFileName = Guid.NewGuid().ToString();//

                var upload = Path.Combine(webRootPath, @"images\ProImage"); //upload to the specified path

                var extension = Path.GetExtension(file[0].FileName); //(file[0].FileName)-accessing 1st image in array list or file


                //delete old immage
                //1st knowing the brand details

                var objFromDb = await _unitOfWork.PopularPro.GetByIdAsync(popularpro.Id);
                if (objFromDb.ProImage != null)
                {

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.ProName.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

            }
            await _unitOfWork.PopularPro.Delete(popularpro);
            await _unitOfWork.SaveAsync();

            TempData["error"] = CommonMessage.RecordDeleted;

            return RedirectToAction(nameof(Index));
        }

        private async Task SaveImageAndSetPath(PopularPro popularPro)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\ProImage");
                var extension = Path.GetExtension(file[0].FileName);

                await DeleteImageIfExists(popularPro.ProImage);

                using (var filesStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    await file[0].CopyToAsync(filesStream);
                }

                popularPro.ProImage = @"\images\ProImage\" + newFileName + extension;
            }
        }

        private async Task DeleteImageIfExists(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string imagePathToDelete = Path.Combine(webRootPath, imagePath.Trim('\\'));
                if (System.IO.File.Exists(imagePathToDelete))
                {
                    await Task.Run(() => System.IO.File.Delete(imagePathToDelete));
                }
            }
        }


    }
}
