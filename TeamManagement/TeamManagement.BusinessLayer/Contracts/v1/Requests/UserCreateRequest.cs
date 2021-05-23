using System.ComponentModel.DataAnnotations;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class UserCreateRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
