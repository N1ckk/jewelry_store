using System.ComponentModel.DataAnnotations;
namespace WebApplication3.Models
{
    public class Product
    {
        [Key]
        public int p_id { get; set; }
        public string p_name { get; set; }
        public int ca_id { get; set; }
        public string p_material { get; set; }
        public int p_sample { get; set; }
        public string p_sex { get; set; }
        public string p_path { get; set; }
        public ICollection<ProductDetails> ProductDetails { get; set; } // Множественная связь
    }
}
