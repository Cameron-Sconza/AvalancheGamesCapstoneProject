using System;
using System.ComponentModel.DataAnnotations;

namespace AvalancheGamesWeb.Models
{
    public class RegistrationModel
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(Constants.MaxPasswordLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.",
            MinimumLength = Constants.MinPasswordLength)]
        [RegularExpression(Constants.PasswordRequirements, ErrorMessage = Constants.PasswordRequirementsMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Verify Password")]
        public string PasswordAgain { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        //public string Message { get; set; }
    }

}