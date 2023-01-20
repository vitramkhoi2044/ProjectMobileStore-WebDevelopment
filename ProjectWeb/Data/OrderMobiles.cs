using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWeb.Data
{
    public class OrderMobiles
    {
        [Key]
        public int ID { get; set; }
        public int MobileID { get; set; }//FK
        [ForeignKey("MobileID")]
        public Mobiles Mobiles { get; set; }
        public int OrderID { get; set; }//FK
        [ForeignKey("OrderID")]
        public Orders Orders { get; set; }
        public int? Quantity { get; set; }
    }
}
