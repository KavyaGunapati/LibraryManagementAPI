using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models.DTOs
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }= string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("Admin|Librarian|Member", ErrorMessage = "Role must be Admin, Librarian, or Member.")]
        public string Role { get; set; }=string.Empty;
    }


}
