using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain.Commands
{
    public class GetAllFacturationsDomainCommand
    {
        public List<Facturation> GetAllFacturations(RéférentielFacturation référentielFacturation)
        {
            return référentielFacturation.GeAllFacturations();
        }
    }
}
