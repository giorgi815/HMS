using Microsoft.AspNetCore.Identity;

namespace HMS.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Manager Manager { get; set; }
        public Guest Guest { get; set; }
        public Admin Admin { get; set; }
    }
}
