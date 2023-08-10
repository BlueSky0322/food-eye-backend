namespace FoodEyeAPI.Models.RequestResponseModels
{
    public class UserRegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserRole { get; set; }
    }
}
