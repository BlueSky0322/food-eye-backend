using FoodEyeAPI.Models.Table;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodEyeAPI.Models.RequestResponseModels
{
    public class CustomerUpdateOrderRequest
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }

    public class SellerUpdateOrderRequest
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
}
