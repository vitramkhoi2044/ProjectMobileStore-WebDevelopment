using ProjectWeb.Data;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjectWeb.Models
{
    public class CheckoutViewModel
    {
        public double discountPercent { get; set; }
        public AppUser User { get; set; }
        public List<CartItemViewModel> Items { get; set; }
    }
}
