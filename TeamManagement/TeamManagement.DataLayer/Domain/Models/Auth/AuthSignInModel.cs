using System.ComponentModel.DataAnnotations;

namespace TeamManagement.DataLayer.Domain.Models.Auth
{
    public class AuthSignInModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
