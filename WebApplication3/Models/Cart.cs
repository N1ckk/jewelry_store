namespace WebApplication3.Models
{
    public class Cart
    {
        public List<int> items_id { get; set; }
        public List<Product> products { get; set; } = new List<Product>();
        public List<CartItem> cartItems { get; set; } = new List<CartItem>();
        public int CartItemCount => productDetailsList.Count;

        public List<ProductDetails> productDetailsList { get; set; } = new List<ProductDetails>();
        public double size { get; set; }
        public int TotalSum { get; set; } // Новое свойство для хранения общей суммы товаров в корзине

        public Cart()
        {
            items_id = new List<int>();
        }
    }
}
