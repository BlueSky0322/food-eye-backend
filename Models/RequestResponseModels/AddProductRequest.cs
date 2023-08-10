using FoodEyeAPI.Models.Table;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodEyeAPI.Models.RequestResponseModels
{
    public class AddProductRequest
    {
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal Price { get; set; }
        public int ShelfLife { get; set; }
        public string Status { get; set; }
        public string ProductImagePath { get; set; }
        public string UserId { get; set; }

    }
}
