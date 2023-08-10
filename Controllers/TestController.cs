using Microsoft.AspNetCore.Mvc;

namespace FoodEyeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {

        [HttpGet("Test")]
        public IActionResult getName()
        {
            var str = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            return Ok(str);
        }
    }
}
