using ClassyAdsServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAds.Entities;
using MyAds.Interfaces;
using MyAds.Models;
using System.Net;

namespace MyAds.Controllers
{
    [ApiController]
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

            return Ok(categories);
        }

        [Authorize]
        [HttpPost("/categories/create")]
        public async Task<IActionResult> CreateCategory(CreateCategoryInput newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _users.GetUserById((int)HttpContext.Items["UserId"]!);

            if (user == null)
            {
                return StatusCode(
                    (int)HttpStatusCode.NotFound, 
                    new ErrorResponse("User not found.", "User account by your active session id not found or maybe is deleted.")
                );
            }

            if (!user.IsSuperAdmin)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized, new ErrorResponse("You are not authorized.", null));
            }

            var parentCategory = newCategory.ParentCategoryId != null ? (await _categories.GetCategoryById(newCategory.ParentCategoryId.Value)) : null;

            var category = new Category
            {
                Name = newCategory.Name,
                Description = newCategory.Description,
                ParentCategoryId = parentCategory?.Id
            };

            await _categories.CreateCategory(category);

            return Ok(category);
        }

        [HttpPost("/categories/update")]
        public async Task<IActionResult> UpdateCategory()
        {

            return Ok();
        }

        [Authorize]
        [HttpDelete("/categories/{id}")]
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
