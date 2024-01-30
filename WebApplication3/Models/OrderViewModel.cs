namespace WebApplication3.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientFName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductMaterial { get; set; }
        public int ProductSample { get; set; }
        public string ProductDescription { get; set; }
        public double ProductSize { get; set; }
        public int ProductPrice { get; set; }
        public string ProductSex { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
