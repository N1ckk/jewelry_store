using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Client
    {
        [Key] 
        public int c_id { get; set; }
        public string c_name { get; set; }
        public string c_surname { get; set; }
        public string c_f_name { get; set; }
        public string c_adress { get; set; }
        public string c_phone_number { get; set; }
    }
}
