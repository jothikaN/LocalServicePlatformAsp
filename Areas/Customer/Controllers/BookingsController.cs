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
using Bookings = LocalServicePlatform.Domain.Models.Bookings;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using LocalServicePlatform.Application.ApplicationConstants;


namespace LocalServicePlatform.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class BookingsController : Controller
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserNameService _userName;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<BookingsController> _logger;
        public BookingsController(ILogger<BookingsController> logger, IBookingsRepository bookingsRepository, IUnitOfWork unitOfWork, IUserNameService userNameService, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager)
        {
            _bookingsRepository = bookingsRepository;
            _unitOfWork = unitOfWork;
            _userName = userNameService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _logger = logger;
        }



        [HttpGet]
        public async Task<IActionResult> MyBookings(string userId)
        {
            try
            {
                // Retrieve the currently logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                { 

                    var userBookings = await _unitOfWork.Bookings.GetByUserId(user.Id);


                    _logger.LogInformation("Number of user bookings retrieved: {BookingsCount}", userBookings.Count);




                    return View(userBookings);
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
                _logger.LogError(ex, "An error occurred while retrieving bookings");
                return View("Error");
            }
        }








        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Bookings> bookings = await _unitOfWork.Bookings.GetAllPost();
            var bookingVM = new BookingVM
            {
                Booking = bookings

            };
            return View(bookingVM);
        }

        [HttpGet]

        public async Task<IActionResult> Details(Guid id)
        //fetching records using id 

        {
            Bookings bookings = await _unitOfWork.Bookings.GetPostById(id);

            bookings.CreatedBy = await _userName.GetUserName(bookings.CreatedBy);

            bookings.ModifiedBy = await _userName.GetUserName(bookings.ModifiedBy);


            return View(bookings);
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
            IEnumerable<SelectListItem> workType = Enum.GetValues(typeof(WorkType))
               .Cast<WorkType>()
               .Select(x => new SelectListItem
               {
                   Text = x.ToString().ToUpper(),
                   Value = ((int)x).ToString()
               });
            IEnumerable<SelectListItem> Location = Enum.GetValues(typeof(Location))
               .Cast<Location>()
               .Select(x => new SelectListItem
               {
                   Text = x.ToString().ToUpper(),
                   Value = ((int)x).ToString()
               });


            BookingVM bookingVM = new BookingVM
            {
                Bookings = new Bookings(),
                ServiceCategoryList = serviceCategoryList,
                ServiceList = serviceList,
                ServiceTypeList = serviceType,
                WorkTypeList = workType,
                LocationList = Location

            };
            return View(bookingVM);

        }


        [HttpPost]
        public async Task<IActionResult> Create(BookingVM bookingVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    // Retrieve the currently logged-in user
                    var user = await _userManager.GetUserAsync(User);

                    if (user != null)
                    {
                        // Assign the user ID to the booking
                        bookingVM.Bookings.UserId = user.Id;
                        //var selectedService = await _unitOfWork.Services.GetByIdAsync(bookingVM.Bookings.ServiceId);
                        // Create the booking
                        await _unitOfWork.Bookings.Create(bookingVM.Bookings);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        // Handle the case where the user is not found (e.g., user not logged in)
                        return RedirectToAction("Login", "Account"); // Redirect to login page
                    }

                }



                TempData["success"] = CommonMessage.RecordCreated;
                // Redirect to the home page
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                _logger.LogError(ex, "An error occurred while creating a booking");
                return View("Error");
            }
        }


        [HttpGet]

        public async Task<IActionResult> Delete(Guid id)
        //fetching records using id 

        {
            Bookings bookings = await _unitOfWork.Bookings.GetByIdAsync(id);
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
            IEnumerable<SelectListItem> workType = Enum.GetValues(typeof(WorkType))
               .Cast<WorkType>()
               .Select(x => new SelectListItem
               {
                   Text = x.ToString().ToUpper(),
                   Value = ((int)x).ToString()
               });
            IEnumerable<SelectListItem> Location = Enum.GetValues(typeof(Location))
               .Cast<Location>()
               .Select(x => new SelectListItem
               {
                   Text = x.ToString().ToUpper(),
                   Value = ((int)x).ToString()
               });


            BookingVM bookingVM = new BookingVM
            {
                Bookings = new Bookings(),
                ServiceCategoryList = serviceCategoryList,
                ServiceList = serviceList,
                ServiceTypeList = serviceType,
                WorkTypeList = workType,
                LocationList = Location

            };
            return View(bookingVM);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                // Retrieve the booking by Id
                var booking = await _unitOfWork.Bookings.GetByIdAsync(id);

                // Ensure the booking exists
                if (booking == null)
                {
                    TempData["error"] = "The record you are trying to delete does not exist.";
                    return RedirectToAction(nameof(Index));
                }

                // Delete the booking
                await _unitOfWork.Bookings.Delete(booking);
                await _unitOfWork.SaveAsync();


                TempData["error"] = "Record deleted successfully.";
                return RedirectToAction(nameof(MyBookings));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the booking");
                return View("Error");
            }
        }
        }
}