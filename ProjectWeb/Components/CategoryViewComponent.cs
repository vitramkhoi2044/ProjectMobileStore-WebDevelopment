using Microsoft.AspNetCore.Mvc;
using ProjectWeb.Data;

namespace ProjectWeb.Component
{
    public class CategoryViewComponent : ViewComponent
    {
        AppDbContext _dbContext;
        public CategoryViewComponent(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BrandCategories> categories = _dbContext.BrandCategories.ToList();
            return View(categories);
        }
    }
}
