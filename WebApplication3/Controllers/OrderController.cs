using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Checkout(string firstName, string lastName, string phone, string address, string paymentMethod, int productId, double productSize, int summ)
        {
            int quant = 1;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                // Проверяем, существует ли клиент в базе данных по имени и фамилии
                var existingClient = _context.client.FirstOrDefault(c => c.c_name == firstName && c.c_surname == lastName);

                int clientId;

                if (existingClient == null)
                {
                    var newClient = new Client
                    {
                        c_name = firstName,
                        c_surname = lastName,
                        c_phone_number = phone,
                        c_adress = address
                        // Добавьте остальные данные клиента, если необходимо
                    };

                    _context.client.Add(newClient);
                    _context.SaveChanges();

                    clientId = newClient.c_id; // Получаем только что созданный c_id клиента
                }
                else
                {
                    clientId = existingClient.c_id; // Получаем c_id существующего клиента
                }

                // Создаем заказ
                var order = new Order
                {
                    c_id = clientId,
                    or_date = DateTime.Now, // текущая дата
                    or_status = paymentMethod
                };

                _context.orders.Add(order);
                _context.SaveChanges();

                // Получаем только что созданный o_id заказа
                int orderId = order.o_id;

                var pdd_id = _context.product_details
                .Where(pd => pd.p_id == productId && pd.pd_size == productSize)
                .Select(pd => pd.pd_id)
                .FirstOrDefault();

                // Заполняем таблицу product_orders
                var productOrder = new ProductOrders
                {
                    pd_id = pdd_id,
                    o_id = orderId,
                    po_quantity = quant,
                    po_summary = summ
                };

                _context.product_orders.Add(productOrder);
                _context.SaveChanges();

                // Ваша логика для создания заказа или перенаправление на страницу "Спасибо за заказ"
            }

            var model = new CheckoutViewModel
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Address = address,
                PaymentMethod = paymentMethod,
                ProductId = productId,
                ProductSize = productSize,
                Summ = summ
            };

            return View(model);
        }

    }

}
