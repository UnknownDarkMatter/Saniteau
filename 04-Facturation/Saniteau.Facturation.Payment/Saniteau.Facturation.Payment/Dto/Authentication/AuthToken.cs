using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Facturation.Payment.Services.Dto.Authentication
{
    public class AuthToken
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string TokenType { get; set; }
        public string AppId { get; set; }
        public string Scope { get; set; }
    }
}
