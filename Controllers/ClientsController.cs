using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCHTI_KURSACH.Models;
using POCHTI_KURSACH.Models.Entities;

namespace POCHTI_KURSACH.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private DatabaseContext _context;

        public ClientsController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Profile()
        {
            IQueryable<ProductsInBagViewModel> bags = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = HttpContext.User.Identity.Name;
                User user = _context.Users.FirstOrDefault(u => u.Login == userName);
                if (user == null) return NotFound();

                bags = _context.Bags.Where(b => b.UserId == user.Id).Select(b =>
                    new ProductsInBagViewModel
                    {
                        Name = b.Product.Name,
                        image = b.Product.image,
                        Id = b.Id,
                        Amount =  b.Amount,
                        ProductId = b.ProductId
                    });

                var prod = await (from b in bags
                            join p in _context.Products
                            on b.ProductId equals p.Id
                            select new
                            {
                                Amount = b.Amount,
                                Price = p.Price,
                                Id = p.Id
                            }).ToListAsync();

                float resultPrice = 0;
                foreach (var item in prod)
                {
                    resultPrice += item.Price*item.Amount;
                }
                ViewBag.ResultPrice = resultPrice;

            }

            return View(await bags.ToListAsync());
        }

        public async Task<IActionResult> RemoveFromBag(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = HttpContext.User.Identity.Name;
                User user = _context.Users.FirstOrDefault(u => u.Login == userName);
                if (user == null) return NotFound();

                Bag bag = _context.Bags.FirstOrDefault(p => p.Id == (int)id);
                if (bag == null) return NotFound();
                Product product = _context.Products.FirstOrDefault(p => p.Id == bag.ProductId);
                product.Amount++;
               
                if (bag.Amount == 1) _context.Bags.Remove(bag);
                else bag.Amount--;
                await _context.SaveChangesAsync();
            }
                return RedirectToAction("ShowAll", "Catalog");
        }

        public async Task<IActionResult> BuyAll()
        {
            IQueryable<ProductsInBagViewModel> bags = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = HttpContext.User.Identity.Name;
                User user = _context.Users.FirstOrDefault(u => u.Login == userName);
                if (user == null) return NotFound();

                bags = _context.Bags.Where(b => b.UserId == user.Id).Select(b =>
                    new ProductsInBagViewModel
                    {
                        Name = b.Product.Name,
                        image = b.Product.image,
                        Id = b.Id,
                        Amount = b.Amount,
                        ProductId = b.ProductId
                    });

                var prod = await (from b in bags
                                  join p in _context.Products
                                  on b.ProductId equals p.Id
                                  select new
                                  {
                                      Amount = b.Amount,
                                      Price = p.Price,
                                      Id = p.Id
                                  }).ToListAsync();

                float resultPrice = 0;
                foreach (var item in prod)
                {
                    resultPrice += item.Price * item.Amount;
                }
                ViewBag.ResultPrice = resultPrice;

            }

            return View(await bags.ToListAsync());
        }

    [HttpGet]
        public IActionResult Buy(int? id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.Id == (int)id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBuy(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = HttpContext.User.Identity.Name;
                User user = _context.Users.FirstOrDefault(u => u.Login == userName);
                if (user == null) return NotFound();

                Bag bag = _context.Bags.FirstOrDefault(b => b.UserId == user.Id && b.ProductId == (int)id);
                if (bag.Amount == 1) _context.Bags.Remove(bag);
                else bag.Amount--;

                Operation operation = new Operation
                {
                    UserId = user.Id,
                    ProductId = (int)id,
                    Date = DateTime.Now
                };

                _context.Operations.Add(operation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Profile", "Clients");
        }
    }
}
