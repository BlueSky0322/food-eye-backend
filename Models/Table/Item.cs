using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodEyeAPI.Models.Table
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        [StringLength(256)]
        public string ItemName { get; set; }
        [StringLength(20)]
        public string ItemType { get; set; }
        public int Quantity { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatePurchased { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateExpiresOn { get; set; }
        [StringLength(256)]
        public string? ImagePath { get; set; }
        [StringLength(10)]
        public string StoredAt { get; set; }
        [StringLength(256)]
        public string? Description { get; set; }

        // Foreign key to reference the User who owns this item
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
