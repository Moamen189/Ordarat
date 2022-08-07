using System.ComponentModel.DataAnnotations;

namespace Ordarat.Dtos
{
    public class RegisterDto
    {
        [Required]

        public string DisplayName { get; set; }

        [Required]
        public string FisrtName { get; set; }

        [Required]

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]

        public string Password { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        public string Street { get; set; }


    }
}
