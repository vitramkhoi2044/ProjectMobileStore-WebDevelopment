using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectWeb.Data;
using ProjectWeb.Models;
using System.Diagnostics;
using System.IO.Pipelines;

namespace ProjectWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;
        
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            List<BrandCategories> categories = _dbContext.BrandCategories.ToList();
            List<Mobiles> mobilePhones = _dbContext.Mobiles.Where(m => m.MobileType.Equals("Smartphone")).ToList();
            List<Mobiles> listTablets = _dbContext.Mobiles.Where(m => m.MobileType.Equals("Tablet")).ToList();
            MyViewModel viewModel = new MyViewModel();
            viewModel.listPhones = mobilePhones;
            viewModel.listTablets = listTablets;
            viewModel.listBrands = categories;
            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}