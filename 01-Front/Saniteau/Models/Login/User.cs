using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Models.Login
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
    }
}
