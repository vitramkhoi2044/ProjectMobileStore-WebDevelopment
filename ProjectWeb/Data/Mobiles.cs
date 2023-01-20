using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWeb.Data
{
    public class Mobiles
    {
        [Key]
        public int MobileID { get; set; }
        public string? MobileName { get; set; }
        public string? Color { get; set; }
        public string? MobileDescription { get; set; }
        public string? Screen { get; set; }
        public int? Ram { get; set; }
        public int? Rom { get; set; }
        public string? Chip { get; set; }
        public string? FrontCamera { get; set; }
        public string? BackCamera { get; set; }
        public int? Battery { get; set; }
        public int? Charger { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? TotalImage { get; set; }
        public int? MobileQuantity { get; set; }
        public string? MobileType { get; set; }
        public int? Price { get; set; }
        public int BrandID { get; set; }//FK
        [ForeignKey("BrandID")]
        public BrandCategories? BrandCategories { get; set; }
        public ICollection<OrderMobiles>? OrderMobiles { get; set; }
    }
}
