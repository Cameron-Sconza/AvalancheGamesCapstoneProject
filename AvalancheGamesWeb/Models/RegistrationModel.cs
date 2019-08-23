using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalancheGamesWeb.Models
{
    public class RegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public string DateOfBirth { get; set; }
    }

}