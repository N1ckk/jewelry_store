using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class PageController : Controller
    {
        private readonly AppDbContext _context; // Предполагается, что это ваш контекст базы данных

        public PageController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult GetPrice(int pId, int pdId)
        {
            var product = _context.product.Include(p => p.ProductDetails).FirstOrDefault(p => p.p_id == pId);
            var price = product?.ProductDetails.FirstOrDefault(pd => pd.pd_id == pdId)?.pd_price;

            return Content(price.ToString());
        }

        public IActionResult GetWeight(int pId, int pdId)
        {
            var product = _context.product.Include(p => p.ProductDetails).FirstOrDefault(p => p.p_id == pId);
            var weight = product?.ProductDetails.FirstOrDefault(pd => pd.pd_id == pdId)?.pd_weight;

            return Content(weight.ToString());
        }


        // Действие для отображения подробной информации о товаре по его идентификатору
        public IActionResult Objc(int id)
        {
            var categories = _context.category.ToList();
            ViewBag.Categories = categories;

            var product = _context.product.Include(p => p.ProductDetails).FirstOrDefault(p => p.p_id == id);

            bool disableSizeSelection = product?.ProductDetails.Any(pd => pd.pd_size == 0) ?? true;

            ViewData["DisableSizeSelection"] = disableSizeSelection;
            ViewData["ProductPrice"] = product?.ProductDetails.FirstOrDefault()?.pd_price ?? 0;
            ViewData["ProductWeight"] = product?.ProductDetails.FirstOrDefault()?.pd_weight ?? 0.0;

            return View(product);
        }
    }
}

