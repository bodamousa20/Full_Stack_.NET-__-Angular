using System.ComponentModel.DataAnnotations;

namespace EcommeraceWebApiPractice.Controllers
{
    public class RegisterDto
    {
        [Required]
        public string username { get; set; }

        [Required]
        public long age { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone must be 11 digits")]
        public string phone { set; get; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}