using EcommeraceWebApiPractice.Entites;
using EcommeraceWebApiPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EcommeraceWebApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AccountUser> manager;
        private readonly TokenService tokenService;


        public AuthController(UserManager<AccountUser> manager, TokenService tokenService)
        {
            this.manager = manager;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AccountUser user = new AccountUser
            {
                UserName = dto.username,
                Email = dto.email,
                age = dto.age,
                PhoneNumber = dto.phone,                

            };
             IdentityResult result = await manager.CreateAsync(user,dto.password);
            if (result.Succeeded)
            {
                return Ok(new { message = "Account Created Successfully", });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("errorsList", item.Description);
                }
                return BadRequest(ModelState);
            }

            //
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            

            // 1 - find user
            var user = await manager.FindByEmailAsync(dto.email);
            if (user == null)
                return NotFound(new { message = "Invalid Email or Password" });

            // 2 - check password
            bool isCorrect = await manager.CheckPasswordAsync(user, dto.password);
            if (!isCorrect)
                return NotFound(new { message = "Invalid Email or Password" });
            UserResponse response = new UserResponse
            {
                id = user.Id,
                age = user.age,
                username = user.UserName,
                email = user.Email,
                phone = user.PhoneNumber
            };
            // 3 - generate token
            return Ok(new
            {
                message = "Login Successfully",
                user = response,
                token = tokenService.GenerateToken(user)
            });
        }


    }


    public class UserResponse
    {
        public string id { set; get; }
        public long age { set; get; }
        public string username { set; get; }
        public string email { set; get; }
        public string phone { set; get; }
    }
}
