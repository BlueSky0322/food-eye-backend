using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodEyeAPI.Models.Table
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [StringLength(256)]
        public string ProductName { get; set; }
        [StringLength(256)]
        public string ProductDesc { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }
        public decimal Price { get; set; }
        public int ShelfLife { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        public string ProductImageURL { get; set; }
        public string ProductS3Key { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
