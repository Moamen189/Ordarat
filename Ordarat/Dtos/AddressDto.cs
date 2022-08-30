using System.ComponentModel.DataAnnotations;

namespace Ordarat.Dtos
{
    public class AddressDto
    {
        
        public string FirstName { get; set; }
        

        public string LastName { get; set; }
        
        public string Country { get; set; }
        [Required]

        public string City { get; set; }

        public string Streeet { get; set; }
    }
}
