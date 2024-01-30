using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class ProductOrders
    {
        [Key] // Определение первичного ключа
        public int po_id { get; set; }

        [ForeignKey("ProductDetails")] // Определение внешнего ключа
        public int pd_id { get; set; }

        [ForeignKey("Order")] // Определение внешнего ключа
        public int o_id { get; set; }
        public int po_quantity { get; set; }
        public int po_summary { get; set; }

        public ProductDetails ProductDetails { get; set; } // Навигационное свойство
        public Order Order { get; set; } // Навигационное свойство
    }
}
