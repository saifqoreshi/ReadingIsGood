using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{

    public class CustomerDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        [MinLength(4, ErrorMessage = "First name must be atleast 5 characters long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(3, ErrorMessage = "Last name must be atleast 3 characters long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(MagicStrings.EmailRegex,
                            ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MinLength(10, ErrorMessage = "Address must be minimum 10 characters long")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MinLength(5, ErrorMessage ="Not a valid phone number")]
        public string PhoneNumber { get; set; }

    }
}