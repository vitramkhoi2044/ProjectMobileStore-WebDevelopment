using ProjectWeb.Data;

namespace ProjectWeb.Models
{
    public class MyViewModel
    {
        public List<Mobiles> listPhones { get; set; }
        public List<Mobiles> listTablets { get; set; }
        public List<Mobiles> listMobileByBrand { get; set; }
        public List<BrandCategories> listBrands { get; set; }
        public string BrandName { get; set; }
    }
}
