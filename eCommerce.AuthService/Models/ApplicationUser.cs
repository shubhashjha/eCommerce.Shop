using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace eCommerce.AuthService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
