using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class GetAbonnéDomainCommand
    {
        public IdAbonné IdAbonné { get; private set; }

        public GetAbonnéDomainCommand(IdAbonné idAbonné)
        {
            IdAbonné = idAbonné;
        }
    }
}
