using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau
{
    public static class Constants
    {
        public class Database
        {
            public const string NomCompteurPompePrincipale = "Compteur de pompe principale";
            public const string NomPompePrincipale = "Pompe principale";

        }

        public static class JWT
        {
            public const int TokenValidityMinutes = 120;
            public const int TokenClockStewMinutes = 0;

            public const string ApiAccessPolicyName = "ApiUser";
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol";
                public const string Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }

    }
}
