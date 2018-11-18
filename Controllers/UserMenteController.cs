using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using startbootstrap_sb_admin.Models;
using startbootstrap_sb_admin.Services;
using Microsoft.Extensions.Logging;
using startbootstrap_sb_admin.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage;

namespace startbootstrap_sb_admin.Controllers
{
    /// <summary>
    /// TODO:本当は権限チェックが必要
    /// </summary>
    [Authorize(Roles ="Admin")]
    public class UserMenteController : Controller
    {
        public UserMenteController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ApplicationDbContext db,
            ILogger<AccountController> logger)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _db;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(_db.Users.ToList());
        }

        public IActionResult Details(String id)
        {
            var model = _db.Users.Where(r => r.Id == id).FirstOrDefault();

            if (model == null) return NotFound();

            return View(model);
        }

        public ActionResult Edit(String id)
        {
            var model = _db.Users.Where(r => r.Id == id).FirstOrDefault();
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(
            [Bind("UserName,TwoFactorEnabled,EmailConfirmed,Email,LockoutEnabled,AccessFailedCount")]
            ApplicationUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View();
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            var model = _db.Users.Where(r => r.Id == id).FirstOrDefault();
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = _db.Users.Where(r => r.Id == id).FirstOrDefault();
            if (user == null) return NotFound();
            _db.Users.Remove(user);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}