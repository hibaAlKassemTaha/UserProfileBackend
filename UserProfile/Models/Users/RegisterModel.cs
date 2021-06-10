using System.ComponentModel.DataAnnotations;

namespace UserProfile.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
       
        [Required]
        public int Salary { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public System.DateTime DateOfBirth { get; set; }
    }
}