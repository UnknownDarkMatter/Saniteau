using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Domain.Commands
{
    public class ObtientDelegantDomainCommand
    {
        public Delegant ObtientDéléguant(RéférentielDelegant référentielDelegant)
        {
            var delegant = référentielDelegant.GetDelegants().FirstOrDefault();
            return delegant;
        }

    }
}
