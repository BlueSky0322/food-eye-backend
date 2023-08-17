using Amazon.S3.Model;
using Amazon.S3;
using FoodEyeAPI.Database;
using FoodEyeAPI.Models;
using FoodEyeAPI.Models.RequestResponseModels;
using FoodEyeAPI.Models.Table;
using Microsoft.AspNetCore.Mvc;
using Amazon;
using Newtonsoft.Json;
using Amazon.S3.Util;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodEyeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class ItemController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public ItemController(DatabaseContext context)
        {
            _context = context;
        }
        private const string s3BucketName = "foodeyebucket";

        private List<string> getValues()
        {
            List<string> values = new List<string>();

            //1. link to appsettings.json and get back the values
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build(); //build the json file

            //2. read the info from json using configure instance
            values.Add(configure["Values:Key1"]);
            values.Add(configure["Values:Key2"]);
            values.Add(configure["Values:Key3"]);

            return values;
        }


        [HttpGet("GetAllItems/{userId}")]
        public ActionResult<IEnumerable<Item>> GetAllItems(string userId)
        {
            // Retrieve items based on the provided userId
            var items = _context.Items.Where(item => item.UserId == userId).ToList();

            return items;
        }

        [HttpGet("GetItemByID/{id}")]
        public ActionResult<Item> GetItemByID(int id)
        {
            var Items = _context.Items.Find(id);

            if (Items == null)
            {
                return NotFound();
            }

            return Items;
        }

        [HttpGet("GetItemsExpiringWithin/{days}/{userId}")]
        public ActionResult<IEnumerable<Item>> GetItemsExpiringWithin(int days, string userId)
        {
            DateTime now = DateTime.Now;
            DateTime targetDate = now.AddDays(days);

            var retrievedItems = _context.Items
                .Where(item => item.DateExpiresOn <= targetDate && item.UserId == userId)
                .ToList();

            return retrievedItems;
        }

        [HttpGet("GetFreshItems/{userId}")]
        public ActionResult<IEnumerable<Item>> GetFreshItems(string userId)
        {
            DateTime now = DateTime.Now;

            var retrievedItems = _context.Items
                .Where(item => item.DateExpiresOn > now && item.UserId == userId)
                .ToList();

            return retrievedItems;
        }

        [HttpPost("AddToS3Bucket")]
        public async Task<IActionResult> AddToS3Bucket(IFormFile itemImage)
        {
            if (itemImage != null && itemImage.Length > 0)
            {
                try
                {
                    List<string> getKeys = getValues();
                    var awsS3client = new AmazonS3Client(getKeys[0], getKeys[1], getKeys[2], RegionEndpoint.USEast1);
                    // Extract the filename from the file path for S3Key
                    var s3Key = "images/selleritems/" + itemImage.FileName;

                    // Upload the image to AWS S3
                    PutObjectRequest uploadRequest = new PutObjectRequest //generate the request
                    {
                        InputStream = itemImage.OpenReadStream(),
                        BucketName = s3BucketName,
                        Key = s3Key,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    await awsS3client.PutObjectAsync(uploadRequest);

                }
                catch (AmazonS3Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }
            }
            return Ok();
        }

        [HttpPost("AddToS3BucketFromCredentialsFile")]
        public async Task<IActionResult> AddToS3BucketFromCredentialsFile(IFormFile itemImage)
        {
            if (itemImage != null && itemImage.Length > 0)
            {
                try
                {
                    
                    var awsS3client = new AmazonS3Client();
                    // Extract the filename from the file path for S3Key
                    var s3Key = "images/selleritems/" + itemImage.FileName;

                    // Upload the image to AWS S3
                    PutObjectRequest uploadRequest = new PutObjectRequest //generate the request
                    {
                        InputStream = itemImage.OpenReadStream(),
                        BucketName = s3BucketName,
                        Key = s3Key,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    await awsS3client.PutObjectAsync(uploadRequest);

                }
                catch (AmazonS3Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }
            }
            return Ok();
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddFEItem(IFormFile? itemImage, [FromForm] string itemData)
        {
            try
            {
                // Convert the JSON data to the AddItemRequest object
                var addItemRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddItemRequest>(itemData);
                var newItem = new Item
                {
                    ItemName = addItemRequest.ItemName,
                    ItemType = addItemRequest.ItemType,
                    Quantity = addItemRequest.Quantity,
                    DatePurchased = addItemRequest.DatePurchased,
                    DateExpiresOn = addItemRequest.DateExpiresOn,
                    StoredAt = addItemRequest.StoredAt,
                    Description = addItemRequest.Description,
                    UserId = addItemRequest.UserId,
                };
                if (itemImage != null && itemImage.Length > 0 && itemImage.FileName != "default-item-image.jpg")
                {
                    try
                    {
                        //    List<string> getKeys = getValues();
                        //    var awsS3client = new AmazonS3Client(getKeys[0], getKeys[1], getKeys[2], RegionEndpoint.USEast1);
                        var awsS3client = new AmazonS3Client();
                        // Extract the filename from the file path for S3Key
                        var s3Key = "images/selleritems/" + itemImage.FileName;
                        // Upload the image to AWS S3
                        PutObjectRequest uploadRequest = new PutObjectRequest //generate the request
                        {
                            InputStream = itemImage.OpenReadStream(),
                            BucketName = s3BucketName,
                            Key = s3Key,
                            CannedACL = S3CannedACL.PublicRead
                        };
                        await awsS3client.PutObjectAsync(uploadRequest);
                        newItem.ItemImageURL = "https://" + s3BucketName + ".s3.amazonaws.com/" + s3Key;
                        newItem.ItemS3Key = s3Key;
                    }
                    catch (AmazonS3Exception ex)
                    {
                        return BadRequest("Error: " + ex.Message);
                    }
                }
                else
                {
                    newItem.ItemImageURL = "https://foodeyebucket.s3.amazonaws.com/images/default-item-image.jpg";
                    newItem.ItemS3Key = "images/default-item-image.jpg";
                }
                _context.Items.Add(newItem);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetItemByID), new { id = newItem.ItemID }, newItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the item.");
            }
        }

        [HttpPut("UpdateItem")]
        public async Task<IActionResult> UpdateItem(IFormFile? itemImage, [FromForm] string itemData)
        {
            var updateItemRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateItemRequest>(itemData);
            var itemToEdit = await _context.Items.FindAsync(updateItemRequest.ItemId);

            if (itemToEdit == null)
            {
                return NotFound();
            }

            if (itemImage != null && itemImage.Length > 0)
            {
                try
                {
                    //List<string> values = getValues();
                    //var awsS3client = new AmazonS3Client(values[0], values[1], values[2], RegionEndpoint.USEast1);// Read the file from the file path

                    var awsS3client = new AmazonS3Client();

                    // Extract the filename from the file path for S3Key
                    var s3Key = "images/selleritems/" + itemImage.FileName;

                    bool isExists = await IsS3FileExists(s3BucketName, s3Key, awsS3client);

                    if (!isExists)
                    {
                        // Upload the image to AWS S3
                        PutObjectRequest uploadRequest = new PutObjectRequest //generate the request
                        {
                            InputStream = itemImage.OpenReadStream(),
                            BucketName = s3BucketName,
                            Key = s3Key,
                            CannedACL = S3CannedACL.PublicRead
                        };

                        await awsS3client.PutObjectAsync(uploadRequest);

                        itemToEdit.ItemImageURL = "https://" + s3BucketName + ".s3.amazonaws.com/" + s3Key;
                        itemToEdit.ItemS3Key = s3Key;
                    }
                    else
                    {
                        itemToEdit.ItemImageURL = "https://" + s3BucketName + ".s3.amazonaws.com/" + s3Key;
                        itemToEdit.ItemS3Key = s3Key;
                    }
                }
                catch (AmazonS3Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }
            }
            
               

            itemToEdit.ItemName = updateItemRequest.ItemName;
            itemToEdit.ItemType = updateItemRequest.ItemType;
            itemToEdit.Quantity = updateItemRequest.Quantity;
            itemToEdit.DatePurchased = updateItemRequest.DatePurchased;
            itemToEdit.DateExpiresOn = updateItemRequest.DateExpiresOn;
            itemToEdit.StoredAt = updateItemRequest.StoredAt;
            itemToEdit.Description = updateItemRequest.Description;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("StandardUpdateItem")]
        public IActionResult StandardUpdateItem(UpdateItemRequest updatedItem)
        {
            var Items = _context.Items.Find(updatedItem.ItemId);

            if (Items == null)
            {
                return NotFound();
            }

            Items.ItemName = updatedItem.ItemName;
            Items.ItemType = updatedItem.ItemType;
            Items.Quantity = updatedItem.Quantity;
            Items.DatePurchased = updatedItem.DatePurchased;
            Items.DateExpiresOn = updatedItem.DateExpiresOn;
            Items.StoredAt = updatedItem.StoredAt;
            Items.Description = updatedItem.Description;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/FoodEyeItems/5
        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var itemToDelete = await _context.Items.FindAsync(id);

            if (itemToDelete == null)
            {
                return NotFound();
            }

            if (itemToDelete.ItemS3Key != "images/default-item-image.jpg")
            {
                try
                {
                    //List<string> getKeys = getValues();
                    //var awsS3client = new AmazonS3Client(getKeys[0], getKeys[1], getKeys[2], RegionEndpoint.USEast1);// Read the file from the file path
                    var awsS3client = new AmazonS3Client();
                    // Extract the filename from the file path for S3Key
                    var s3Key = itemToDelete.ItemS3Key;

                    // Upload the image to AWS S3
                    DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest //generate the request
                    {
                        BucketName = s3BucketName,
                        Key = s3Key,
                    };

                    await awsS3client.DeleteObjectAsync(deleteObjectRequest);
                    _context.Items.Remove(itemToDelete);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }
                catch (AmazonS3Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }
            }
            else
            {
                _context.Items.Remove(itemToDelete);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

        private async Task<bool> IsS3FileExists(string bucketName, string fileName, AmazonS3Client client)
        {
            try
            {
                var s3Client = client;
                var request = new GetObjectMetadataRequest()
                {
                    BucketName = bucketName,
                    Key = fileName,
                };

                var response = await s3Client.GetObjectMetadataAsync(request);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"IsFileExists: Error during checking if file exists in s3 bucket: {JsonConvert.SerializeObject(ex)}");
                return false;
            }
        }
    }
}
