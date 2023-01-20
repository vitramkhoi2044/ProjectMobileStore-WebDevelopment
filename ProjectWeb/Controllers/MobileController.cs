using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectWeb.Data;
using ProjectWeb.Models;

namespace ProjectWeb.Controllers
{
    public class MobileController : Controller
    {
        private readonly ILogger<MobileController> _logger;
        private readonly AppDbContext _dbContext;

        public MobileController(ILogger<MobileController> logger, AppDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Detail(int id)
        {
            Mobiles mobile = _dbContext.Mobiles.SingleOrDefault(m => m.MobileID == id);
            return View(mobile);
        }
        
        public IActionResult MobileByBrand(int id)
        {
            List<Mobiles> mobiles = _dbContext.Mobiles.Where(m => m.BrandID.Equals(id)).ToList();
            MyViewModel myViewModel = new MyViewModel();
            myViewModel.listMobileByBrand = mobiles;
            myViewModel.BrandName = _dbContext.BrandCategories.SingleOrDefault(b => b.BrandID.Equals(id)).BrandName;
            return View(myViewModel);
        }
    }
}
