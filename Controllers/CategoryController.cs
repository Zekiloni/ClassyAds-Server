using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClassyAdsServer.Models;
using ClassyAdsServer.Entities;
using ClassyAdsServer.Interfaces;
using System.Net;

namespace ClassyAdsServer.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public CategoryController(ICategoryService categoryService, IUserService userService) {
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet("/categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetCategories();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpGet("/categories/{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound("CategoryNotFound");
            }

            return Ok(category);
        }

        [Authorize]
        [HttpPost("/categories/create")]
        public async Task<IActionResult> CreateCategory(CreateCategoryInput newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserById((int)HttpContext.Items["UserId"]!);

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

            var parentCategory = newCategory.ParentCategoryId != null ? (await _categoryService.GetCategoryById(newCategory.ParentCategoryId.Value)) : null;

            var category = new Category
            {
                Name = newCategory.Name,
                Description = newCategory.Description,
                ParentCategoryId = parentCategory?.Id
            };

            await _categoryService.CreateCategory(category);

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
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategory(category);

            return Ok();
        }
    }
}
