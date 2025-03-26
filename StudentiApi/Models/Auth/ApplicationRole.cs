using Microsoft.AspNetCore.Identity;

namespace StudentiApi.Models.Auth
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}

