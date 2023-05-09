using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAds.Entities;
using MyAds.Interfaces;
using MyAds.Models;

namespace MyAds.Controllers
{
    [ApiController]
    [Route("api/{controller}")]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categories;
        private readonly IUserService _users;

        public CategoryController(ICategoryService categories, IUserService users) {
            _categories = categories;
            _users = users;
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

        [HttpPost("/categories/create")]
        [Authorize]
        public async Task<IActionResult> CreateCategory(CreateCategoryInput newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _users.GetUserById((int)HttpContext.Items["UserId"]!);

            if (user == null)
            {
                return NotFound();
            }

            if (!user.IsSuperAdmin)
            {
                return Unauthorized();
            }

            var parentCategory = newCategory.ParentCategoryId != null ? (await _categories.GetCategoryById(newCategory.ParentCategoryId.Value)) : null;

            var category = new Category
            {
                Name = newCategory.Name,
                Description = newCategory.Description,
                ParentCategoryId = parentCategory?.Id
            };

            return Ok(category);
        }

        [HttpDelete("/categories/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categories.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            await _categories.DeleteCategory(category);

            return Ok();
        }
    }
}
