using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Worker 
    {
        [Key]
        public int w_id { get; set; }
        public string w_login { get; set; }
        public string w_pass { get; set; }
    }
}
