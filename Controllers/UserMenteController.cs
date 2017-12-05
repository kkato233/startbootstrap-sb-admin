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

            return View();
        }
    }
}