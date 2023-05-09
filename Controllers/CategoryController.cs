using Microsoft.AspNetCore.Mvc;
using MyAds.Interfaces;

namespace MyAds.Controllers
{
    [ApiController]
    [Route("api/{controller}")]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categories;

        public CategoryController(ICategoryService categories) {
            _categories = categories;
        }

        [HttpGet("/categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categories.GetCategories();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(_categories);
        }
    }
}
