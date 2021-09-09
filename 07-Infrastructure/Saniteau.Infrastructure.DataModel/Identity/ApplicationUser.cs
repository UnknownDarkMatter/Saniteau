using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Saniteau.Infrastructure.DataModel.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }

        public ApplicationUser() { }

        public ApplicationUser(string userName, string prenom, string nom)
        {
            this.UserName = userName;
            this.Prenom = prenom;
            this.Nom = nom;
        }
    }
}
