using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMS.Data;
using DMS.Service;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DMS.Controllers
{
    /*
     * CATEGORY CONTROLLER MAIN CLASS
     * HAS ACCESS TO: ADMIN,USER
     */

    [Authorize(Roles="Admin,User")]
    public class CategoryController : Controller
    {
        private CategoryService _categoryService;
        public CategoryController(DMSContext context)
        {
            _categoryService = new CategoryService(context);
        }

        /*
         * CATEGORY LIST
         */
        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var categories = _categoryService.GetAll(email);
            return View(categories);
        }

        /*
         * SHOW CATEGORY CREATE FORM
         */
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /*
         * NEW CATEGORY CREATE
         */
        [HttpPost]
        public IActionResult Create(Category category)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var status = _categoryService.CreateCategory(category,email);
            if (status)
            {
                ViewBag.success = "Created successfully";
            }
            else
            {
                ViewBag.error = "Something was wrong.";
            }
            return View();
        }

        /*
         * CATEGORY DELETE BY ID
         */

        public IActionResult Delete(int id)
        {
            var status = _categoryService.DeleteCategory(id);
            if (status)
            {
                TempData["success"] = "Deleted successfully";
            }
            else
            {
                TempData["Error"] = "Error Occurred";
            }
          return RedirectToAction("Index");
        }
    }
}