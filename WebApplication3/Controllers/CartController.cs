using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Newtonsoft.Json;
using System.Xml.Serialization;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting.Server;
using System.Drawing;

namespace WebApplication3.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CartController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult AddToCart(int productId, int selectedSizeId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();

            var query = from pd in _dbContext.product_details
                        join p in _dbContext.product on pd.p_id equals p.p_id
                        where pd.p_id == productId && pd.pd_id == selectedSizeId
                        select new ProductDetails
                        {
                            pd_price = pd.pd_price,
                            pd_size = pd.pd_size
                        };

            // Получение результата запроса
            var productDetails = query.FirstOrDefault(); // Получаем первый элемент или null

            if (productDetails != null)
            {
                cart.productDetailsList.Add(productDetails); // Добавляем ProductDetails в список корзины
                cart.TotalSum = cart.productDetailsList.Sum(pd => pd.pd_price); // Обновляем общую сумму
                ViewBag.TotalSum = cart.TotalSum; // Передаем общую сумму в представление
            }
            else
            {
                var result = _dbContext.product_details
                    .Where(pd => pd.p_id == productId && pd.pd_size == 0)
                    .Select(pd => pd.pd_id)
                    .ToList();

                int selectedSizeId1 = result.FirstOrDefault();

                var query1 = from pd in _dbContext.product_details
                             join p in _dbContext.product on pd.p_id equals p.p_id
                             where pd.p_id == productId && pd.pd_id == selectedSizeId1
                             select new ProductDetails
                             {
                                 pd_price = pd.pd_price,
                                 pd_size = pd.pd_size
                             };

                var productDetails1 = query1.FirstOrDefault();

                cart.productDetailsList.Add(productDetails1); // Добавляем ProductDetails в список корзины
                cart.TotalSum = cart.productDetailsList.Sum(pd => pd.pd_price); // Обновляем общую сумму
                ViewBag.TotalSum = cart.TotalSum; // Передаем общую сумму в представление

            }

            var prod = _dbContext.product.FirstOrDefault(p => p.p_id == productId);
            if (prod != null)
            {
                cart.products.Add(prod);
            }
            else
            {
                // Обработка отсутствия товара с указанным productId в базе данных
            }


            cart.items_id.Add(productId); // Добавляем productId в список элементов корзины
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            var cartItemCount = cart.productDetailsList.Count;
            ViewBag.CartItemCount = cartItemCount;

            return RedirectToAction("Cartt", "Cart");
        }

        public IActionResult CartItemCount()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");
            var cartItemCount = cart != null ? cart.productDetailsList.Count : 0;
            return Ok(cartItemCount); // Возврат количества товаров в корзине
        }


        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();

            // Находим индекс товара в списке по его productId
            int index = cart.items_id.IndexOf(productId);

            if (index != -1)
            {
                // Удаляем товар из списка по найденному индексу
                cart.items_id.RemoveAt(index);
                cart.products.RemoveAt(index);
                cart.productDetailsList.RemoveAt(index);
                cart.TotalSum = cart.productDetailsList.Sum(pd => pd.pd_price); // Обновляем общую сумму
                ViewBag.TotalSum = cart.TotalSum; // Передаем общую сумму в представление

                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("Cartt", "Cart");
        }

        [HttpPost]
        public IActionResult Checkout(string firstName, string lastName, string fatherName, string phone, string address, List<CartItem> cartItems)
        {
            string paymentMethod = "Післяоплата";
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                // Проверяем, существует ли клиент в базе данных по имени и фамилии
                var existingClient = _dbContext.client.FirstOrDefault(c => c.c_name == firstName && c.c_surname == lastName && c.c_f_name == fatherName);

                int clientId;

                if (existingClient == null)
                {
                    var newClient = new Client
                    {
                        c_id = _dbContext.client.Count() + 1,
                        c_name = firstName,
                        c_surname = lastName,
                        c_f_name = fatherName,
                        c_phone_number = phone,
                        c_adress = address
                        // Добавьте остальные данные клиента, если необходимо
                    };

                    _dbContext.client.Add(newClient);
                    _dbContext.SaveChanges();

                    clientId = newClient.c_id; // Получаем только что созданный c_id клиента
                }
                else
                {
                    clientId = existingClient.c_id; // Получаем c_id существующего клиента
                }

                // Создаем заказ
                var order = new Order
                {
                    o_id = _dbContext.orders.Count() + 1,
                    c_id = clientId,
                    or_date = DateTime.Now, // текущая дата
                    or_status = paymentMethod
                };

                _dbContext.orders.Add(order);
                _dbContext.SaveChanges();

                // Получаем только что созданный o_id заказа
                int orderId = order.o_id;

                
                int startingProductId = _dbContext.product_orders.Count() + 1; // Начальное значение идентификатора

                foreach (var cartItem in cartItems)
                {
                    var pdd_id = _dbContext.product_details
                        .Where(pd => pd.p_id == cartItem.p_id && pd.pd_size == cartItem.pd_size)
                        .Select(pd => pd.pd_id)
                        .FirstOrDefault();

                    var product = _dbContext.product_details.FirstOrDefault(p => p.pd_id == pdd_id);
                    product.pd_quantity -= 1;

                    var productOrder = new ProductOrders
                    {
                        po_id = startingProductId++, // Используем начальное значение и увеличиваем его
                        pd_id = pdd_id,
                        o_id = orderId,
                        po_quantity = 1,
                        po_summary = cartItem.pd_price
                    };

                    // Очистка корзины после успешного оформления заказа
                    HttpContext.Session.Remove("Cart");

                    _dbContext.product_orders.Add(productOrder);
                    _dbContext.SaveChanges();
                }


                // Ваша логика для создания заказа или перенаправление на страницу "Спасибо за заказ"
                var model = new CheckoutViewModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                    Address = address,
                    PaymentMethod = paymentMethod,
                    CartItems = cartItems // передача списка товаров в модель представления
                };

                return View(model);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Cartt()
        {
            var categories = _dbContext.category.ToList();
            ViewBag.Categories = categories;
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart();
            return View(cart);
        }
    }
}
