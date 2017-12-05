using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using startbootstrap_sb_admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace startbootstrap_sb_admin.Controllers
{
    [Authorize]
    public class SetupController : Controller
    {
        public SetupController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public async Task<IActionResult> Index()
        {
            if ((await _userManager.GetUsersInRoleAsync("Admin")).Count() > 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(String q = null)
        {
            var user = await _userManager.GetUserAsync(User);
            
            if ((await _roleManager.FindByNameAsync("Admin")) == null)
            {
                var result2 = await _roleManager.CreateAsync(new IdentityRole("Admin"));

                if (!result2.Succeeded)
                {
                    return new RedirectToActionResult("Index", "Home", null);
                }
            }

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Error ";

                return View();
            }
        }
    }
}