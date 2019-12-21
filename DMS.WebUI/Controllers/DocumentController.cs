using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMS.Data;
using DMS.Service;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace DMS.Controllers
{

    /*
     * DOCUMENT CONTROLLER MAIN CLASS
     * HAS ACCESS TO: USER,ADMIN
     */
    [Authorize(Roles= "User,Admin")]
    public class DocumentController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly DocumentService _documentService;
        private CategoryService _categoryService;

        public DocumentController(
            IHostingEnvironment appEnvironment,
            DMSContext context )
        {
            _appEnvironment = appEnvironment;
            _documentService =new DocumentService(context);
            _categoryService = new CategoryService(context);
        }
        
        /*
         * GET OWN DOCUMENTS 
         */
        public async Task<IActionResult> Index(string str, int page = 1)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var documentList = await _documentService.GetList(email, str);
            int pageSize = 10;
            return View(await PaginatedList<DocumentViewModel>.CreateAsync(documentList.AsNoTracking(), page, pageSize));
        }

        /*
         * SHOW DOCUMENT UPLOAD FORM
         */ 
        public IActionResult Create()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            ViewBag.categories = _categoryService.GetAll(email);
            return View();
        }

        /*
         * UPLOAD NEW DOCUMENT
         */ 
        [HttpPost]
        public IActionResult Create(IFormFile file, Document document)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            ViewBag.categories = _categoryService.GetAll(email);
            string pathRoot = _appEnvironment.WebRootPath;
            var documentUploadRespose = _documentService.Upload(file, pathRoot, document, email);
            if (documentUploadRespose.ContainsKey("error"))
            {
                ViewBag.error = documentUploadRespose["error"];
            } else
            {
                ViewBag.success = documentUploadRespose["success"];
            }
            return View();
        }

        /*
         * DOWNLOAD DOCUMENT
         */
        public async Task<ActionResult> DownloadAsync(int id)
        {
            int documentId = (int) id;
            int userId = (int) HttpContext.Session.GetInt32("UserId");
            var status = _documentService.DocumentPermissionRule(userId, documentId);
            if (status) {
                string filePath = _documentService.GetPath(userId, documentId);
                string fileName = _documentService.GetName(userId, documentId);
                return await this.ReturnDocumentFileAsync(filePath, fileName);
            } else
            {
                TempData["Error"] = "Docuement permission failed";
            }
            return RedirectToAction("Index");
        }

        /*
         * RETURN FILE
         */

        public async Task<FileResult> ReturnDocumentFileAsync(string filePath, string fileName)
        {
            var path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\Documents\\", fileName
                           );

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));

        }

        /*
         * GET CONTENT TYPES
         */

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        
        /*
         * GET MIME TYPES
         */ 
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

    }
}