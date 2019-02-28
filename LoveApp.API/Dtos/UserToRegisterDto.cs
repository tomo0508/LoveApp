using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserToRegisterDto
    {
        [Required]
        public string    userName { get; set; }
        
        [Required]
        [StringLength (8, MinimumLength =4, ErrorMessage="Password must be between 4 and 8 char")]
        public string password { get; set; }
    }
}