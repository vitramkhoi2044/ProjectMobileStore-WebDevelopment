using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWeb.Data
{
    public class Promotions
    {
        [Key]
        public int PromotionID { get; set; }
        public string? DiscountCode { get; set; }
        public int? PercentDiscount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
