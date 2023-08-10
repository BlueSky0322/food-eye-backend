using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace FoodEyeAPI.Models.Table
{
    public class User
    {
        [Key]
        [PersonalData]
        [StringLength(36)] // Set the string length to 36 characters (32 hex digits and 4 hyphens) to store the Guid as a string
        public string UserID { get; set; }
        
        [PersonalData]
        [StringLength(50)]
        public string Email { get; set; }

        [PersonalData]
        [StringLength(256)]
        public string Password { get; set; }

        [PersonalData]
        [StringLength(50)]
        public string Name { get; set; }

        [PersonalData]
        public int Age { get; set; }

        [PersonalData]
        [StringLength(100)]
        public string Address { get; set; }

        [PersonalData]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        [PersonalData]
        [StringLength(10)]
        public string UserRole { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
