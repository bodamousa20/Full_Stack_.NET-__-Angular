using Ecom.Core.Entites;
using EcommeraceWebApiPractice.Data.Dto;
using EcommeraceWebApiPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ExceptionServices;

namespace EcommeraceWebApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory services;
       // private readonly IMapper mapper;
        public CategoryController(ICategory services) //,IMapper _mapper)
        {
            this.services = services;
           // mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> getAllCategory()
        {
            var categories = await services.GetAllAsync();
            return Ok(new { message = "success", data = categories });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> getCategoryById(int id)
        {
            var category = await services.GetById(id);
            if (category == null)
                return NotFound(new { message = "Product Not found" });
            return Ok(new { message = "Success", data = category });
        }

        [HttpPost]
        public async Task<IActionResult> createCategory([FromBody] CategoryDto dto)
        {/*
            //first map dto to entity
            Category category = mapper.Map<Category>(dto);

            var createdCat = await services.AddAsync(category);

            //map again from category to dto
            return CreatedAtAction(
                                    nameof(getCategoryById),
                                    new { id = createdCat.id },
                                    mapper.Map<CategoryDto>(createdCat)
                                    );
           
           
            */
            return null;
        }

        [HttpPut("{catId:int}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto dto , int catId)
        {/*
            
            //map it to entity
            Category category =  mapper.Map<Category>(dto);
            var categoryUpdated   =  await services.UpdateAsync(catId, category);
            if (categoryUpdated == null)
                return NotFound();
            return Ok(new { message = "Updated successfully", data = categoryUpdated });
            */
            return null;

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool IsDeleted = await services.DeleteAsync(id);
            if (!IsDeleted)
                return NotFound();

            return Ok(new { message = "Deleted" });
        }

    }


}
