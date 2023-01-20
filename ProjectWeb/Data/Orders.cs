using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace ProjectWeb.Data
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Status { get; set; }
        [StringLength(450)]
        public string CustomerID { get; set; }//FK
        public ICollection<OrderMobiles> OrderMobiles { get; set; }
    }
}
