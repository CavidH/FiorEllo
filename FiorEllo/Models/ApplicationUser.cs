using Microsoft.AspNetCore.Identity;

namespace FiorEllo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActived { get; set; }
    }
}