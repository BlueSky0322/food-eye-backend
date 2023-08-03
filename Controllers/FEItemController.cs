using FoodEyeAPI.Database;
using FoodEyeAPI.Models;
using FoodEyeAPI.Models.RequestResponseModels;
using FoodEyeAPI.Models.Table;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodEyeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodEyeItemsController : ControllerBase
    {
        private readonly FoodEyeDbContext _context;

        public FoodEyeItemsController(FoodEyeDbContext context)
        {
            _context = context;
        }
        // GET: api/FoodEyeItems
        [HttpGet("GetAllItems")]
        public ActionResult<IEnumerable<FoodEyeItem>> GetAllItems()
        {
            return _context.FoodEyeItems.ToList();
        }

        // GET: api/FoodEyeItems/5
        [HttpGet("GetItemByID/{id}")]
        public ActionResult<FoodEyeItem> GetFoodEyeItem(int id)
        {
            var foodEyeItem = _context.FoodEyeItems.Find(id);

            if (foodEyeItem == null)
            {
                return NotFound();
            }

            return foodEyeItem;
        }

        // GET: api/FoodEyeItems/GetItemsExpiringWithin/{days}
        [HttpGet("GetItemsExpiringWithin/{days}")]
        public ActionResult<IEnumerable<FoodEyeItem>> GetItemsExpiringWithin(int days)
        {
            DateTime now = DateTime.Now;
            DateTime targetDate = now.AddDays(days);

            var retrievedItems = _context.FoodEyeItems
                .Where(item => item.DateExpiresOn <= targetDate)
                .ToList();

            return retrievedItems;
        }

        // GET: api/FoodEyeItems/GetNotExpiredItems
        [HttpGet("GetFreshItems")]
        public ActionResult<IEnumerable<FoodEyeItem>> GetFreshItems()
        {
            DateTime now = DateTime.Now;

            var retrievedItems = _context.FoodEyeItems
                .Where(item => item.DateExpiresOn > now)
                .ToList();

            return retrievedItems;
        }

        // POST: api/FoodEyeItems
        [HttpPost("AddFEItem")]
        public async Task<IActionResult> AddFEItem(FoodEyeItem foodEyeItem)
        {
            _context.FoodEyeItems.Add(foodEyeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoodEyeItem), new { id = foodEyeItem.ItemID }, foodEyeItem);
        }

        // PUT: api/FoodEyeItems/5
        [HttpPut("UpdateFEItem/{id}")]
        public IActionResult EditItem(int id, FEUpdateItemRequest updatedItem)
        {
            var foodEyeItem = _context.FoodEyeItems.Find(id);

            if (foodEyeItem == null)
            {
                return NotFound();
            }

            foodEyeItem.ItemName = updatedItem.ItemName;
            foodEyeItem.ItemType = updatedItem.ItemType;
            foodEyeItem.Quantity = updatedItem.Quantity;
            foodEyeItem.DatePurchased = updatedItem.DatePurchased;
            foodEyeItem.DateExpiresOn = updatedItem.DateExpiresOn;
            foodEyeItem.ImagePath = updatedItem.ImagePath;
            foodEyeItem.StoredAt = updatedItem.StoredAt;
            foodEyeItem.Description = updatedItem.Description;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/FoodEyeItems/5
        [HttpDelete("DeleteFEItem/{id}")]
        public IActionResult DeleteItem(int id)
        {
            var foodEyeItem = _context.FoodEyeItems.Find(id);

            if (foodEyeItem == null)
            {
                return NotFound();
            }

            _context.FoodEyeItems.Remove(foodEyeItem);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
