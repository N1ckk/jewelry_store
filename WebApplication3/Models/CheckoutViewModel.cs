namespace WebApplication3.Models
{
    public class CheckoutViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public int ProductId { get; set; }
        public double ProductSize { get; set; }
        public int Summ { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
