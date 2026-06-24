using Microsoft.AspNetCore.Identity;

namespace EcommeraceWebApiPractice.Entites
{
    public class AccountUser : IdentityUser
    {
        public long age { set; get; }
        public cart Cart { get; set; }

    }
}