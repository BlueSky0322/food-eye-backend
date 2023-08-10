using FoodEyeAPI.Models.Table;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodEyeAPI.Models.RequestResponseModels
{
    public class UpdateProductRequest
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal Price { get; set; }
        public int ShelfLife { get; set; }
        public string Status { get; set; }
        public string? ImagePath { get; set; }
    }
}
