using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectWeb.Data;
using ProjectWeb.Models;

namespace ProjectWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<MobileController> _logger;
        private readonly AppDbContext _dbContext;

        public ShoppingCartController(ILogger<MobileController> logger, AppDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpPost]
        public IActionResult AddToCart([FromForm] int id)
        {
            Mobiles mobile = _dbContext.Mobiles.Where(p => p.MobileID == id).FirstOrDefault();
            if (mobile == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.mobile.MobileID == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItemViewModel() { quantity = 1, mobile = mobile });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddToCartWithQuantity([FromForm] int id, [FromForm] int quantity)
        {
            Mobiles mobile = _dbContext.Mobiles.Where(p => p.MobileID == id).FirstOrDefault();
            if (mobile == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.mobile.MobileID == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity += quantity;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItemViewModel() { quantity = quantity, mobile = mobile });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            return Ok();
        }

        [HttpPost]
        public IActionResult UpdateCart([FromForm] int id, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.mobile.MobileID == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }
        [HttpPost]
        public IActionResult RemoveCart([FromForm] int id)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.mobile.MobileID == id);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }
            SaveCartSession(cart);
            return Ok();
        }

        public IActionResult Cart()
        {
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel();
            viewModel.cartItems = GetCartItems();
            var session = HttpContext.Session;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Cart(ShoppingCartViewModel viewModel)
        {
            if(viewModel.discountCode!= null)
            {
                Promotions promotion = _dbContext.Promotions.FirstOrDefault(p => p.DiscountCode.Equals(viewModel.discountCode.Trim()));
                DateTime date = DateTime.Now;
                if (date >= promotion.StartDate && date <= promotion.EndDate)
                {
                    double discountPercent = Double.Parse(promotion.PercentDiscount.ToString()) / 100;
                    var session = HttpContext.Session;
                    session.SetString("DiscountPercent", discountPercent.ToString());
                }
            }
            return RedirectToAction(nameof(Cart));
        }

        public IActionResult Checkout()
        {
            var session = HttpContext.Session;

            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            string discount = session.GetString("DiscountPercent");
            if(discount != null)
            {
                checkoutViewModel.discountPercent = Double.Parse(discount);
            }
            else
            {
                checkoutViewModel.discountPercent = 0;
            }
            checkoutViewModel.Items = GetCartItems();
            string userName = User.Identity.Name;
            checkoutViewModel.User = _dbContext.Users.FirstOrDefault(u => u.Email.Equals(userName));
            return View(checkoutViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel viewModel)
        {
            var session = HttpContext.Session;
            string discount = session.GetString("DiscountPercent");
            if (discount != null)
            {
                viewModel.discountPercent = Double.Parse(discount);
            }
            else
            {
                viewModel.discountPercent = 0;
            }

            viewModel.Items = GetCartItems();
            if(viewModel.Items != null)
            {
                MakeNewOrder(viewModel);
                double total = 0;

                Orders order = _dbContext.Orders.OrderByDescending(o => o.OrderID).First();
                foreach (var item in viewModel.Items)
                {
                    double amount = Double.Parse(item.mobile.Price.ToString()) * Double.Parse(item.quantity.ToString());
                    total += amount;

                    OrderMobiles mobile = new OrderMobiles
                    {
                        OrderID = order.OrderID,
                        MobileID = item.mobile.MobileID,
                        Quantity = item.quantity
                    };
                    _dbContext.OrderMobiles.Add(mobile);
                }
                total = total - (total * viewModel.discountPercent);
                order.OrderID = order.OrderID;
                order.TotalPrice = total;
                order.CreateDate = order.CreateDate;
                order.Status = "Successful";
                order.CustomerID = order.CustomerID;
                _dbContext.Orders.Update(order);
                _dbContext.SaveChanges();
                session.Remove(CARTKEY);
                session.Remove("DiscountPercent");
            }
            return RedirectToAction(nameof(Successful));
        }

        public IActionResult Successful()
        {
            return View();
        }

        //Session handle
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItemViewModel> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItemViewModel>>(jsoncart);
            }
            return new List<CartItemViewModel>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItemViewModel> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

        void MakeNewOrder(CheckoutViewModel viewModel)
        {
            Orders order = new Orders
            {
                CreateDate = DateTime.Now,
                Status = "Processing",
                CustomerID = viewModel.User.Id
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }
        
    }
}
