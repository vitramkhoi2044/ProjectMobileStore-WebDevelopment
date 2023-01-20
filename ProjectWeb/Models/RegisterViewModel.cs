using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjectWeb.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^.*[a-zA-Z]", ErrorMessage = "First Name must have a-z and A-Z")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^.*[a-zA-Z]", ErrorMessage = "Last Name must have a-z and A-Z")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public string? Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"^.*[a-zA-Z0-9]", ErrorMessage = "Last Name must have a-z and A-Z")]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Required]
        [MinLength(10), MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
