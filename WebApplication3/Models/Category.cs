using System.ComponentModel.DataAnnotations;
namespace WebApplication3.Models
{
    public class Category
    {
        [Key]
        public int ca_id { get; set; }
        public string ca_name { get; set; }
    }
}
