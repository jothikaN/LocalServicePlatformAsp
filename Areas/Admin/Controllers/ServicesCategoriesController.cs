using LocalServicePlatform.Application.ApplicationConstants;
using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.Models;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Drawing2D;

namespace FinProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesCategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ServicesCategoriesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)    //injecting constructor
                                                                                                     //handling  the page which has been uploading  when submitting
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            List<ServiceCategories> serviceCategories = await _unitOfWork.ServiceCategories.GetAllAsync();
            return View(serviceCategories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCategories serviceCategories)
        {
            {
                if (ModelState.IsValid)
                {

                    await _unitOfWork.ServiceCategories.Create(serviceCategories);
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
            ServiceCategories serviceCategories = await _unitOfWork.ServiceCategories.GetByIdAsync(id);
            return View(serviceCategories);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        //fetching records using id 

        {
            ServiceCategories serviceCategories = await _unitOfWork.ServiceCategories.GetByIdAsync(id); //fetching record which i choose(id) that is equal to db id
            return View(serviceCategories);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(ServiceCategories serviceCategories)
        {

            if (ModelState.IsValid)
            {
                await _unitOfWork.ServiceCategories.Update(serviceCategories);
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
            ServiceCategories serviceCategories = await _unitOfWork.ServiceCategories.GetByIdAsync(id);
            return View(serviceCategories);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ServiceCategories serviceCategories)
        {
            
            await _unitOfWork.ServiceCategories.Delete(serviceCategories);
            await _unitOfWork.SaveAsync();

            TempData["error"] = CommonMessage.RecordDeleted;

            return RedirectToAction(nameof(Index));
        }

    }
}
