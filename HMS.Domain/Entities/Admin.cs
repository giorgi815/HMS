
namespace HMS.Domain.Entities
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser AppliationUser { get; set; }
    }
}
