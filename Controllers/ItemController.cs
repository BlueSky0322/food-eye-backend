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
    public class ItemController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ItemController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: api/FoodEyeItems
        [HttpGet("GetAllItems")]
        public ActionResult<IEnumerable<Item>> GetAllItems()
        {
            return _context.Items.ToList();
        }

        // GET: api/FoodEyeItems/5
        [HttpGet("GetItemByID/{id}")]
        public ActionResult<Item> GetFoodEyeItem(int id)
        {
            var Items = _context.Items.Find(id);

            if (Items == null)
            {
                return NotFound();
            }

            return Items;
        }

        // GET: api/FoodEyeItems/GetItemsExpiringWithin/{days}
        [HttpGet("GetItemsExpiringWithin/{days}")]
        public ActionResult<IEnumerable<Item>> GetItemsExpiringWithin(int days)
        {
            DateTime now = DateTime.Now;
            DateTime targetDate = now.AddDays(days);

            var retrievedItems = _context.Items
                .Where(item => item.DateExpiresOn <= targetDate)
                .ToList();

            return retrievedItems;
        }

        // GET: api/FoodEyeItems/GetNotExpiredItems
        [HttpGet("GetFreshItems")]
        public ActionResult<IEnumerable<Item>> GetFreshItems()
        {
            DateTime now = DateTime.Now;

            var retrievedItems = _context.Items
                .Where(item => item.DateExpiresOn > now)
                .ToList();

            return retrievedItems;
        }

        // POST: api/FoodEyeItems
        [HttpPost("AddFEItem")]
        public async Task<IActionResult> AddFEItem(Item Items)
        {
            _context.Items.Add(Items);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoodEyeItem), new { id = Items.ItemID }, Items);
        }

        // PUT: api/FoodEyeItems/5
        [HttpPut("UpdateFEItem/{id}")]
        public IActionResult EditItem(int id, FEUpdateItemRequest updatedItem)
        {
            var Items = _context.Items.Find(id);

            if (Items == null)
            {
                return NotFound();
            }

            Items.ItemName = updatedItem.ItemName;
            Items.ItemType = updatedItem.ItemType;
            Items.Quantity = updatedItem.Quantity;
            Items.DatePurchased = updatedItem.DatePurchased;
            Items.DateExpiresOn = updatedItem.DateExpiresOn;
            Items.ImagePath = updatedItem.ImagePath;
            Items.StoredAt = updatedItem.StoredAt;
            Items.Description = updatedItem.Description;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/FoodEyeItems/5
        [HttpDelete("DeleteFEItem/{id}")]
        public IActionResult DeleteItem(int id)
        {
            var Items = _context.Items.Find(id);

            if (Items == null)
            {
                return NotFound();
            }

            _context.Items.Remove(Items);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
