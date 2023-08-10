using System.Collections.Generic;
using System.Linq;
using Amazon.S3.Model;
using Amazon.S3;
using FoodEyeAPI.Database;
using FoodEyeAPI.Models.RequestResponseModels;
using FoodEyeAPI.Models.Table;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Amazon;
using Newtonsoft.Json;

namespace FoodEyeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProductController(DatabaseContext context)
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

        [HttpGet("CustomerGetAllProducts")]
        public ActionResult<IEnumerable<CustomerProductReponse>> CustomerGetAllProducts()
        {
            var products = _context.Products
                .Include(p => p.User) // Include the related User entity
                .Select(p => new CustomerProductReponse
                {
                    ProductId = p.ProductID,
                    ProductName = p.ProductName,
                    ProductDesc = p.ProductDesc,
                    Price = p.Price,
                    ShelfLife = p.ShelfLife,
                    Status = p.Status,
                    ProductImageURL = p.ProductImageURL,
                    SellerId = p.UserId,
                    SellerName = p.User.Name // Get the user's name instead of UserId
                })
                .ToList();

            return products;
        }


        // GET: Product/GetAllProducts
        [HttpGet("GetAllProducts/{userId}")]
        public ActionResult<IEnumerable<Product>> GetAllProducts(string userId)
        {
            var products = _context.Products.Where(p => p.UserId == userId).ToList();
            return products;
        }

        // GET: Product/GetProductById/5
        [HttpGet("GetProductById/{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: Product/AddProduct
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(IFormFile? productImage, [FromForm] string productData)
        {
            try
            {
                // Convert the JSON data to the AddItemRequest object
                var addProductRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddProductRequest>(productData);

                var newProduct = new Product
                {
                    UserId = addProductRequest.UserId,
                    ProductName = addProductRequest.ProductName,
                    ProductDesc = addProductRequest.ProductDesc,
                    DateAdded = addProductRequest.DateAdded,
                    Price = addProductRequest.Price,
                    ShelfLife = addProductRequest.ShelfLife,
                    Status = addProductRequest.Status,
                };

                if (productImage != null && productImage.Length > 0 && productImage.FileName != "default-item-image.jpg")
                {
                    try
                    {
                        List<string> getKeys = getValues();
                        var awsS3client = new AmazonS3Client(getKeys[0], getKeys[1], getKeys[2], RegionEndpoint.USEast1);// Read the file from the file path

                        // Extract the filename from the file path for S3Key
                        var s3Key = "images/sellerproducts/" + productImage.FileName;

                        // Upload the image to AWS S3
                        PutObjectRequest uploadRequest = new PutObjectRequest //generate the request
                        {
                            InputStream = productImage.OpenReadStream(),
                            BucketName = s3BucketName,
                            Key = s3Key,
                            CannedACL = S3CannedACL.PublicRead
                        };

                        await awsS3client.PutObjectAsync(uploadRequest);

                        newProduct.ProductImageURL = "https://" + s3BucketName + ".s3.amazonaws.com/" + s3Key;
                        newProduct.ProductS3Key = s3Key;

                    }
                    catch (AmazonS3Exception ex)
                    {
                        return BadRequest("Error: " + ex.Message);
                    }
                }
                else
                {
                    newProduct.ProductImageURL = "https://foodeyebucket.s3.amazonaws.com/images/default-item-image.jpg";
                    newProduct.ProductS3Key = "default-item-image.jpg";
                }

                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductID }, newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the item.");
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateItem(IFormFile? productImage, [FromForm] string productData)
        {
            var updateProductRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateProductRequest>(productData);
            var productToEdit = await _context.Products.FindAsync(updateProductRequest.ProductId);

            if (productToEdit == null)
            {
                return NotFound();
            }

            if (productImage != null && productImage.Length > 0)
            {
                try
                {
                    List<string> values = getValues();
                    var awsS3client = new AmazonS3Client(values[0], values[1], values[2], RegionEndpoint.USEast1);// Read the file from the file path

                    // Extract the filename from the file path for S3Key
                    var s3Key = "images/sellerproducts/" + productImage.FileName;

                    bool isExists = await IsS3FileExists(s3BucketName, s3Key, awsS3client);

                    if (!isExists)
                    {
                        // Upload the image to AWS S3
                        PutObjectRequest uploadRequest = new PutObjectRequest //generate the request
                        {
                            InputStream = productImage.OpenReadStream(),
                            BucketName = s3BucketName,
                            Key = s3Key,
                            CannedACL = S3CannedACL.PublicRead
                        };

                        await awsS3client.PutObjectAsync(uploadRequest);

                        productToEdit.ProductImageURL = "https://" + s3BucketName + ".s3.amazonaws.com/" + s3Key;
                        productToEdit.ProductS3Key = s3Key;
                    }
                    else
                    {
                        productToEdit.ProductImageURL = "https://" + s3BucketName + ".s3.amazonaws.com/" + s3Key;
                        productToEdit.ProductS3Key = s3Key;
                    }
                }
                catch (AmazonS3Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }
            }

            productToEdit.ProductName = updateProductRequest.ProductName;
            productToEdit.ProductDesc = updateProductRequest.ProductDesc;
            productToEdit.DateAdded = updateProductRequest.DateAdded;
            productToEdit.Price = updateProductRequest.Price;
            productToEdit.ShelfLife = updateProductRequest.ShelfLife;
            productToEdit.Status = updateProductRequest.Status;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: Product/UpdateProduct/5
        [HttpPut("UpdateProduct/{id}")]
        public IActionResult UpdateProduct(int id, UpdateProductRequest updatedProduct)
        {
            var Products = _context.Products.Find(id);
            if (Products == null)
            {
                return NotFound();
            }

            Products.ProductName = updatedProduct.ProductName;
            Products.ProductDesc = updatedProduct.ProductDesc;
            Products.DateAdded = updatedProduct.DateAdded;
            Products.Price = updatedProduct.Price;
            Products.ShelfLife = updatedProduct.ShelfLife;
            Products.Status = updatedProduct.Status;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: Product/DeleteProduct/5
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            if (productToDelete.ProductS3Key != "images/default-item-image.jpg")
            {
                try
                {
                    List<string> getKeys = getValues();
                    var awsS3client = new AmazonS3Client(getKeys[0], getKeys[1], getKeys[2], RegionEndpoint.USEast1);// Read the file from the file path

                    // Extract the filename from the file path for S3Key
                    var s3Key = productToDelete.ProductS3Key;

                    // Upload the image to AWS S3
                    DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest //generate the request
                    {
                        BucketName = s3BucketName,
                        Key = s3Key,
                    };

                    await awsS3client.DeleteObjectAsync(deleteObjectRequest);
                    _context.Products.Remove(productToDelete);
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
                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();

                return Ok();
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
