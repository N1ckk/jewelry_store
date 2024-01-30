using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class OrderManagementController : Controller
    {
        private readonly AuthenticationService _authService;
        private readonly AppDbContext _dbContext;

        public OrderManagementController(AuthenticationService authService, AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("n9pl5kBA4p4J5iVl7Z66s1KZnEo4nXp2J5fh5WMq6U0=FgHSdWbGfserSfgvJWWfgGdgGhSfSrEEEKfdSPnMQerR/gfjgjfdghdfhgnke43dfbwSflGry789faerdgKb54Sf")] // Задание кастомного маршрута для метода Login
        public IActionResult Login()
        {
            return View(); // Возвращает представление Login.cshtml
        }

        [Route("n9pl5kBA4p4J5iVl7Z66s1KZnEo4nXp2J5fh5WMq6U0=FgHSdWbGfserSfgvJWWfgGdgGhSfSrEEEKfdSPnMQerR/n535rfGkftqOdgfhrkKgdQErdgphGtEPjKdgHHf2Jh")] // Задание кастомного маршрута для метода Orders
        public IActionResult Orders()
        {
            if(User.Identity.IsAuthenticated)
            {
                DateTime twoWeeksAgo = DateTime.Now.AddDays(-14);

                var result = _dbContext.orders
                .Where(o => o.or_date >= twoWeeksAgo)
                .Join(_dbContext.product_orders,
                    o => o.o_id,
                    po => po.o_id,
                    (o, po) => new { Order = o, ProductOrder = po })
                .Join(_dbContext.product_details,
                    op => op.ProductOrder.pd_id,
                    pd => pd.pd_id,
                    (op, pd) => new { op.Order, op.ProductOrder, ProductDetail = pd })
                .Join(_dbContext.product,
                    opd => opd.ProductDetail.p_id,
                    p => p.p_id,
                    (opd, p) => new { opd.Order, opd.ProductOrder, opd.ProductDetail, Product = p })
                .Join(_dbContext.client,
                    opdp => opdp.Order.c_id,
                    c => c.c_id,
                    (opdp, c) => new { opdp.Order, opdp.ProductOrder, opdp.ProductDetail, opdp.Product, Client = c })
                .Select(opdp => new OrderViewModel
                {
                    OrderId = opdp.Order.o_id,
                    ProductName = opdp.Product.p_name,
                    ProductMaterial = opdp.Product.p_material,
                    ProductSample = opdp.Product.p_sample,
                    ProductSize = opdp.ProductDetail.pd_size,
                    ProductPrice = opdp.ProductDetail.pd_price,
                    ProductSex = opdp.Product.p_sex,
                    OrderDate = opdp.Order.or_date,
                    OrderStatus = opdp.Order.or_status,
                    ClientName = opdp.Client.c_name,
                    ClientSurname = opdp.Client.c_surname,
                    ClientFName = opdp.Client.c_f_name,
                    ClientPhoneNumber = opdp.Client.c_phone_number,
                    ClientAddress = opdp.Client.c_adress// Добавьте информацию о клиенте в OrderViewModel
                                                        // Другие свойства клиента
                })
                .ToList();
                return View(result);
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("n9pl5kBA4p4J5iVl7Z66s1KZnEo4nXp2J5fh5WMq6U0=FgHSdWbGfserSfgvJWWfgGdgGhSfSrEEEKfdSPnMQerR/gfjgjfdghdfhgnke43dfbwSflGry789faerdgKb54Sf")] // Задание кастомного маршрута для метода Login по HTTP POST
        public IActionResult Login(string username, string password)
        {
            bool isAuthenticated = _authService.ValidateCredentials(username, password);

            if (isAuthenticated)
            {
                // Создание утверждений (claims)
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username), // Добавление имени пользователя в утверждения
            // Другие утверждения, если требуется
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Дополнительные свойства аутентификации, если нужно
                };

                // Установка куки аутентификации
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Orders"); // Перенаправление на страницу управления заказами
            }
            else
            {
                return RedirectToAction("Login"); // Перенаправление на страницу входа при неуспешной аутентификации
            }
        }
    }
}
