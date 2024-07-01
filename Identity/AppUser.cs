using Microsoft.AspNetCore.Identity;

namespace semissssloan.Identity
{
    public class AppUser : IdentityUser
    {
        public string? Fullname { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
    }
}