
using Ecom.Core.Entites;
using EcommeraceWebApiPractice.Data.Dto;
using EcommeraceWebApiPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommeraceWebApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productServices;

        public ProductController(IProduct _productServices )
        {
            this.productServices = _productServices;

        }

        [HttpGet]
        public async Task<IActionResult> getAllProducts([FromQuery]filterDto filter, [FromQuery]int pageNumber =1)
        {
            
            IReadOnlyList<Product>products  = await productServices.GetAllAsync(filter,pageNumber);
            List<productResponse> responses = new List<productResponse>();
            foreach (Product p in products)
            {
                productResponse response = new productResponse
                {
                    ID = p.id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    thunmailImage = p.Photos?.FirstOrDefault()?.imageUrl ?? ""
                };
                responses.Add(response);
            }
            int totalProduct = productServices.countProduct();
            return Ok(new { message = "Success", Data = responses,pagenumber=pageNumber,pagesize=15,total= totalProduct });
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> getProductById(int id)
        {
            Product? product = await productServices.GetById(id);
            if (product == null)
            return NotFound();
            List<PhotoDto> photos = new List<PhotoDto>();
            foreach (var photo in product.Photos ?? [])
            {
                PhotoDto photoDto = new PhotoDto
                {
                    id = photo.id,
                    ImageUrl = photo.imageUrl,

                };
                photos.Add(photoDto);
            }
            productDetailsResponse productDetailsResponse = new productDetailsResponse
            {
                ID = product.id,
                Description = product.Description,
                Name = product.Name,
                Photos = photos,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
            return Ok(new { message = "Success", Data = productDetailsResponse });
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> deleteProduct(int id)
        {
            bool deleted =  await productServices.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return Ok(new { message = "Deleted Successfully" });
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            [FromBody] productRequestDto dto)
        {
            Product product = new Product
            {
                id = id,
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Price = dto.Price
            };

            Product? updatedProduct =
                await productServices.UpdateAsync(id, product);

            if (updatedProduct == null)
            {
                return NotFound(new
                {
                    message =
                        "Product not found or Category Id is invalid"
                });
            }

            productResponse response =
                new productResponse
                {
                    ID = updatedProduct.id,
                    Name = updatedProduct.Name,
                    Description = updatedProduct.Description,
                    Price = updatedProduct.Price,
                    CategoryId = updatedProduct.CategoryId,
                    thunmailImage =
                        updatedProduct.Photos?
                        .FirstOrDefault()?.imageUrl ?? ""
                };

            return Ok(new
            {
                message = "Updated Successfully",
                data = response
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] productRequestDto dto)
        {
            Product product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Price = dto.Price
            };
            Product createdPro = await productServices.AddAsync(product);
            if (createdPro == null)
                return BadRequest(new { message = "Category Id is Invalid" });
            productResponse response = new productResponse
            {
                ID = createdPro.id,
                Description = createdPro.Description,
                Name = createdPro.Name,
                Price = createdPro.Price,
                CategoryId = createdPro.CategoryId
            };
           
           return CreatedAtAction(nameof(getProductById), new {id = createdPro.id},
               new { message = "Created Successfully", data = response});
            

        }
    }
}
