using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{
    public partial class CustomerMetaData
    {

        [Required(ErrorMessage = "First Name is required")]
        [MinLength(5)]
        public string FirstName;

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50)]
        public string LastName;

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                            ErrorMessage = "Please enter a valid email address")]
        public string Email;

        [Required(ErrorMessage = "Address is required")]
        [MinLength(10)]
        public string Address;

    }
}
