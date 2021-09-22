using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Domain.Commands
{
    public class ObtientDelegantDomainCommand
    {
        public List<Delegant> ObtientDéléguants(RéférentielDelegant référentielDelegant)
        {
            var delegants = référentielDelegant.GetDelegants().ToList();
            return delegants;
        }

    }
}
