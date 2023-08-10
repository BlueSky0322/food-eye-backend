namespace FoodEyeAPI.Models.RequestResponseModels
{
    public class CustomerProductReponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal Price { get; set; }
        public int ShelfLife { get; set; }
        public string Status { get; set; }
        public string ProductImageURL { get; set; }
        public string SellerId { get; set; }
        public string SellerName { get; set; }
    }
}
