using System.ComponentModel.DataAnnotations;

namespace ProjectWeb.Data
{
    public class BrandCategories
    {
        [Key]
        public int BrandID { get; set; }
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }
        public ICollection<Mobiles>? Mobiles {get; set; }
    }
}
