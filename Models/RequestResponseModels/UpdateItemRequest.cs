using System.ComponentModel.DataAnnotations;

namespace FoodEyeAPI.Models.RequestResponseModels
{
    public class UpdateItemRequest
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public int Quantity { get; set; }
        public DateTime DatePurchased { get; set; }
        public DateTime DateExpiresOn { get; set; }
        public string? ImagePath { get; set; }
        public string StoredAt { get; set; }
        public string? Description { get; set; }
    }
}
