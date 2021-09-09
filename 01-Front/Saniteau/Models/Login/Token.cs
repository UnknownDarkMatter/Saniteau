using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Models.Login
{
    public class Token
    {
        public string Id { get; set; }
        public string Auth_token { get; set; }
        public int Expires_in_seconds { get; set; }

        public Token(string id, string auth_token, int expires_in_seconds)
        {
            Id = id;
            Auth_token = auth_token;
            Expires_in_seconds = expires_in_seconds;
        }
    }
}
