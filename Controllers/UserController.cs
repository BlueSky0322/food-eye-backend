using FoodEyeAPI.Database;
using FoodEyeAPI.Models.RequestResponseModels;
using FoodEyeAPI.Models.Table;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FoodEyeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UserController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetEmail/{userId}")]
        public IActionResult GetEmail(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(new { Email = user.Email });
        }

        // POST: User/Login
        [HttpPost("Login")]
        public IActionResult Login(UserLoginRequest loginRequest)
        {
            var hashedPassword = "";
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password));
                hashedPassword = Convert.ToBase64String(hashedBytes);
            }

            var user = _context.Users.SingleOrDefault(u => u.Email == loginRequest.Email && u.Password == hashedPassword);

            if (user == null)
            {
                return NotFound("Invalid email or password");
            }

            // Return the UserId and UserRole as a response object
            var response = new LoginResponse
            {
                UserId = user.UserID,
                UserRole = user.UserRole
            };

            return Ok(response);
        }


        // POST: User/Register
        [HttpPost("Register")]
        public IActionResult Register(UserRegisterRequest registerRequest)
        {
            // Check if user with the same email already exists
            var existingUser = _context.Users.SingleOrDefault(u => u.Email == registerRequest.Email);
            if (existingUser != null)
            {
                return Conflict("User with this email already exists");
            }

            // Create a new user
            var newUser = new User
            {
                UserID = Guid.NewGuid().ToString(),
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                Age = registerRequest.Age,
                Address = registerRequest.Address,
                DateOfBirth = registerRequest.DateOfBirth,
                UserRole = registerRequest.UserRole, 
            };

            // Hash the password using SHA256
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(registerRequest.Password));
                newUser.Password = Convert.ToBase64String(hashedBytes);
            }

            // Add the user to the database
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

    }
}
