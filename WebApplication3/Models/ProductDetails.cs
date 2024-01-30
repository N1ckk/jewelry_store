using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication3.Models
{
    public class ProductDetails
    {
        [Key]
        public int pd_id { get; set; }
        public int p_id { get; set; } // Внешний ключ
        public double pd_size { get; set; }
        public double pd_weight{ get; set; }
        public int pd_quantity { get; set; }
        public int pd_price { get; set; }

        [ForeignKey("p_id")]
        public Product Product { get; set; }
    }
}
