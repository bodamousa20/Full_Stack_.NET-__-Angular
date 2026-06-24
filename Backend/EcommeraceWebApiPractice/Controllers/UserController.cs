using Azure;
using Ecom.Core.Entites;
using Ecom.infrrastructure.Data;
using EcommeraceWebApiPractice.Data.Dto;
using EcommeraceWebApiPractice.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommeraceWebApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AccountUser> manager;
        private readonly AppDbContext _context;

        public UserController(UserManager<AccountUser> _manager, AppDbContext context)
        {
            this.manager = _manager;
            _context = context;
        }

        //favourite products 
        //get profile ,update profile 
        // 
        [HttpGet("profile")]
        public async Task<IActionResult> UserProfileAsync()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await manager.FindByIdAsync(UserId);
            if (user == null)
                return BadRequest();
            else
            {
                UserResponse response = new UserResponse
                {
                    id = user.Id,
                    age = user.age,
                    username = user.UserName,
                    email = user.Email,
                    phone = user.PhoneNumber,

                };
                return Ok(new { message = "suceess", data = response });
            }

        }
        [HttpGet("favourites")]
        public IActionResult getFavouriteProducts()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Favourite userFav = _context.favourites.Include(p => p.products).ThenInclude(p => p.Photos).Where(p => p.userId == UserId).FirstOrDefault();
            if (userFav == null || userFav.products == null)
                return Ok(new { message = "No favourites found", data = new List<productResponse>() });

            List<productResponse> res = new List<productResponse>();
            foreach (var fav in userFav.products)
            {
                productResponse response = new productResponse
                {
                    ID = fav.id,
                    Description = fav.Description,
                    Name = fav.Name,
                    Price = fav.Price,
                    CategoryId = fav.CategoryId,
                    thunmailImage = fav.Photos?.ElementAt(0)?.imageUrl
                };
                res.Add(response);
            }

            FavouriteResponseDto dto = new FavouriteResponseDto
            {
                userId = UserId,
                products = res
            };
            return Ok(new { message = "suceess", data = dto });
        }

        [HttpPost("favourites/{productId:int}")]
        public IActionResult AddToFavourites(int productId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // 1. Find product
            var product = _context.products.Find(productId);
            if (product == null) return NotFound("Product not found");

            // 2. Find or create favourite
            var favourite = _context.favourites
                .Include(p => p.products)
                .FirstOrDefault(p => p.userId == UserId);

            if (favourite == null)
            {
                favourite = new Favourite
                {
                    userId = UserId,
                    products = new List<Product>()
                };
                _context.favourites.Add(favourite);
            }

            // 3. Check duplicate
            if (favourite.products.Any(p => p.id == productId))
                return BadRequest(new { message = "Already in favourites" });

            // 4. Add + Save
            favourite.products.Add(product);
            _context.SaveChanges();

            return Ok(new { message = "Added to favourites" });
        }



        [HttpPost("remove-favourite/{productId:int}")]
        public IActionResult removeFromFavourite(int productId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // 1. Find product
            var product = _context.products.Find(productId);
            if (product == null) return NotFound("Product not found");

            // 2. Find or create favourite
            var favourite = _context.favourites
                .Include(p => p.products)
                .FirstOrDefault(p => p.userId == UserId);

            if (favourite == null)
            {
                return BadRequest();
            }

            favourite.products.Remove(product);
            _context.products.Remove(product);
            _context.SaveChanges();

            return Ok(new { message = "removed from favourites" });
        }
    }
}
