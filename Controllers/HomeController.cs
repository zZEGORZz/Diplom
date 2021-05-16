using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POCHTI_KURSACH.Models;
using POCHTI_KURSACH.Models.Entities;
using POCHTI_KURSACH.Models.ViewModels;
using SelectPdf;

namespace POCHTI_KURSACH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
             return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Operations()
        {
            IQueryable<Operation> prod = _context.Operations;

            return View(prod.ToList());
        }
        public IActionResult Task3()
        {
            IQueryable<Product> prod = _context.Products;

            return View(prod.ToList());
        }

        public IActionResult Task4()
        {
            IQueryable<Task4Model> prod = (from u in _context.Users
                                           join o in _context.Operations
                                           on u.Id equals o.UserId
                                           join p in _context.Products
                                           on o.ProductId equals p.Id
                                           select new Task4Model
                                           {
                                               ProductName = p.Name,
                                               UserName = u.Name,
                                               OperationId = o.Id
                                           });
            return View(prod.ToList());
        }
        public IActionResult GeneratePdf(string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");

            HtmlToPdf oHtmlToPdfd = new HtmlToPdf();
            PdfDocument oPdfDocument = oHtmlToPdfd.ConvertHtmlString(html);
            byte[] pdf = oPdfDocument.Save();
            oPdfDocument.Close();

            return File(
                pdf,
                "application/pdf",
                "Reportlist.pdf"
                );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
