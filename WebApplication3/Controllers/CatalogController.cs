using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppDbContext _context;

        public CatalogController(AppDbContext context)
        {
            _context = context;
        }

        // Действие для отображения товаров в каталоге с возможностью сортировки
        public IActionResult Catalog(string sortOrder)
        {
            var categories = _context.category.ToList();
            ViewBag.Categories = categories;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParam"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["DateSortParam"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["DiscountSortParam"] = sortOrder == "discount" ? "discount_desc" : "discount";

            var products = _context.product.AsQueryable();

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.p_name);
                    break;
            }

            return View(products.ToList());
        }
    }
}
