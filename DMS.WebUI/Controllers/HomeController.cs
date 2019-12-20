using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMS.Models;
using DMS.Data;
using DMS.Service;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace DMS.Controllers
{
    /*
     * HOME CONTROLLER MAIN CLASS
     */
    [Authorize]
    public class HomeController : Controller
    {
        /*
         * HOMEPAGE
         */
        public IActionResult Index()
        {
            return View();
        }

        /*
         * ERROR
         */
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
