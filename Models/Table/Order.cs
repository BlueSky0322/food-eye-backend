using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodEyeAPI.Models.Table
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public string OrderDetails { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
        [StringLength(10)]
        public string OrderStatus { get; set; }
    }
}
