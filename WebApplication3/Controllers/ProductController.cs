using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Products(int categoryId, string sortOrder, string sex)
        {
            var categories = _context.category.ToList();
            ViewBag.Categories = categories;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["SelectedSex"] = sex;

            var category = _context.category.FirstOrDefault(c => c.ca_id == categoryId);
            ViewBag.CategoryName = category?.ca_name;

            var productsInCategory = _context.product
                .Include(p => p.ProductDetails)
                .Where(p => p.ca_id == categoryId)
                .ToList();

            switch (sortOrder)
            {
                case "name_desc":
                    productsInCategory = productsInCategory.OrderByDescending(p => p.p_name).ToList();
                    break;
                case "price":
                    productsInCategory = productsInCategory.OrderBy(p => p.ProductDetails.FirstOrDefault()?.pd_price).ToList();
                    break;
                case "price_desc":
                    productsInCategory = productsInCategory.OrderByDescending(p => p.ProductDetails.FirstOrDefault()?.pd_price).ToList();
                    break;
                default:
                    productsInCategory = productsInCategory.OrderBy(p => p.p_name).ToList();
                    break;
            }

            if (!string.IsNullOrEmpty(sex))
            {
                productsInCategory = productsInCategory.Where(p => p.p_sex.ToLower() == sex.ToLower()).ToList();
            }

            return View(productsInCategory);
        }

        public IActionResult Catalog(string sortOrder, string sex, int pageNumber = 1)
        {
            var categories = _context.category.ToList();
            ViewBag.Categories = categories;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["SelectedSex"] = sex;

            var allProducts = _context.product.Include(p => p.ProductDetails).ToList();

            if (!string.IsNullOrEmpty(sex))
            {
                allProducts = allProducts.Where(p => p.p_sex.ToLower() == sex.ToLower()).ToList();
            }

            int pageSize = 12;
            var paginatedProducts = allProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Применение фильтров и сортировки только к отображаемым товарам
            switch (sortOrder)
            {
                case "name_desc":
                    paginatedProducts = paginatedProducts.OrderByDescending(p => p.p_name).ToList();
                    break;
                case "price":
                    paginatedProducts = paginatedProducts.OrderBy(p => p.ProductDetails.FirstOrDefault()?.pd_price).ToList();
                    break;
                case "price_desc":
                    paginatedProducts = paginatedProducts.OrderByDescending(p => p.ProductDetails.FirstOrDefault()?.pd_price).ToList();
                    break;
                default:
                    paginatedProducts = paginatedProducts.OrderBy(p => p.p_name).ToList();
                    break;
            }

            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)allProducts.Count() / pageSize);
            ViewData["NameSortParm"] = sortOrder; // Сохраняем порядок сортировки для передачи обратно в ссылки пагинации

            return View(paginatedProducts);
        }
    }
}
