using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DMS.Data;
using DMS.Service;
using Microsoft.Extensions.Configuration;

namespace DMS.Controllers
{
    /*
     * USER CONTROLLER MAIN CLASS
     * HAS ACCESS TO:ADMIN
     */
    [Authorize(Policy= "Admin")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(DMSContext _context, IConfiguration _config)
        {
            _userService = new UserService(_context, _config);
        }
       
        /*
         * GET LIST OF USERS
         */
        public IActionResult Index()
        {
           var users=  _userService.GetAll();
            return View(users);
        }

        /*
         * NEW USER CREATE FORM
         */
        public IActionResult Create()
        {
            return View();
        }

        /*
         * CREATE NEW USER
         */
        [HttpPost]
        public IActionResult Create(User user)
        {
            var status = _userService.Create(user);
            if (status)
            {
                ViewBag.success = "Created successfully";
            }
            else
            {
                ViewBag.error = "Error Occurred";
            }
            return View();
        }

        /*
         * DELETE USER BY ID
         */
        public IActionResult Delete(int id)
        {
            var status = _userService.Delete(id);
            if (status)
            {
                ViewBag.success = "Deleted successfully";
            }
            else
            {
                ViewBag.error = "Error Occurred";
            }
            return RedirectToAction("Index");
        }


    }
}