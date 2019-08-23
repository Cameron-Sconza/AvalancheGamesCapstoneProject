using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalancheGamesWeb.Models
{
    public class LoginModel
    {
        public string UserEmail {get; set;}
        public string Password {get; set;}
        public string Message {get; set;}
        public string ReturnURL {get; set;}
    }
}