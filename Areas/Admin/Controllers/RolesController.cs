using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager ;

        public RolesController(RoleManager<IdentityRole> rolemanager) 
        {   
           _roleManager  = rolemanager;
        }
        public IActionResult Index()
        {
            var roles=_roleManager.Roles;
            return View(roles);
        }

        [HttpGet]

        public IActionResult Create() 
        
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if(!_roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            {
            _roleManager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
            }

            return RedirectToAction("Index");
        
        }
    }

}
