using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Order
    {
        [Key] // Определение первичного ключа
        public int o_id { get; set; }

        [ForeignKey("Client")] // Определение внешнего ключа
        public int c_id { get; set; }
        public DateTime or_date { get; set; }
        public string or_status { get; set; }

        public Client Client { get; set; } // Навигационное свойство
    }
}
