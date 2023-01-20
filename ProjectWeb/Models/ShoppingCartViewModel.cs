namespace ProjectWeb.Models
{
    public class ShoppingCartViewModel
    {
        public List<CartItemViewModel> cartItems { get; set; }
        public string discountCode { set; get; }
    }
}
