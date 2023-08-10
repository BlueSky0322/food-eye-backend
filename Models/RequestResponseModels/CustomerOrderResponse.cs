using FoodEyeAPI.Models.Table;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodEyeAPI.Models.RequestResponseModels
{
    public class CustomerOrderResponse
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string SellerName { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
