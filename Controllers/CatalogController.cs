using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POCHTI_KURSACH.Models;
using POCHTI_KURSACH.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using POCHTI_KURSACH.Models.ViewModels;

namespace POCHTI_KURSACH.Controllers
{
    public class CatalogController : Controller
    {
        private DatabaseContext _context;

        public CatalogController(DatabaseContext context)
        {

            _context = context;
        }

        public async Task<IActionResult> ShowAll(int? categoryId, string search)
        {
            List<Category> CList = await _context.Categories.ToListAsync();
            CList.Insert(0, new Category { Name = "все", Id = 0});
            SelectList SList = new SelectList(CList, "Id", "Name");
            ViewBag.CategoryList = SList;

            IQueryable<Product> prod = _context.Products.Where(p => p.Amount > 0);

            if (!string.IsNullOrEmpty(search))
            {
                prod = prod.Where(p => p.Name.Contains(search));
            }
            if (categoryId != null)
            {
                if (categoryId != 0)
                {
                    prod = prod.Where(p => p.CategoryId == categoryId);
                }
            }

            return View(await prod.ToListAsync());
        }
        public IActionResult Detail(int? id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == (int)id);
            if (product != null) return View(product);
            else return NotFound();
        }
        [Authorize]
        public  async Task<IActionResult> AddToBag(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = HttpContext.User.Identity.Name;
                User user = _context.Users.FirstOrDefault(u => u.Login == userName);
                if (user == null) return NotFound();

                Product product = _context.Products.FirstOrDefault(p => p.Id == (int)id);
                if (product == null) return NotFound();
                product.Amount--;

                Bag bag = new Bag
                {
                    UserId = user.Id,
                    ProductId = product.Id,
                    Amount = 1
                };
                if (_context.Bags.Any(b => b.UserId == bag.UserId && b.ProductId == bag.ProductId))
                {
                    Bag b = _context.Bags.FirstOrDefault(b => b.UserId == bag.UserId && b.ProductId == bag.ProductId);
                    b.Amount++;
                }
                else 
                    _context.Bags.Add(bag);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ShowAll", "Catalog");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            List<Category> CList = await _context.Categories.ToListAsync();
            CList.Insert(0, new Category { Name = "все", Id = 0 });
            SelectList SList = new SelectList(CList, "Id", "Name");
            ViewBag.CategoryList = SList;
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductModel model)
        {


            _context.Products.Add(new Product
            {
                Name = model.Name,
                Price = model.Price,
                Amount = model.Amount,
                Garant = model.Garant,
                CategoryId = model.CategoryId,
                image = model.image

            });
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowAll", "Catalog");
        }
    }
}