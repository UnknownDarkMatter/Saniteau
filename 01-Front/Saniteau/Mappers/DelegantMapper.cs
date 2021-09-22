using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Mappers
{
    public static class DelegantMapper
    {
        public static Models.Delegation.Delegant Map(DSP.Contract.Model.Delegant délégant)
        {
            if (délégant == null) { return null; }

            var dateContrat = délégant.DateContrat.ToString("dd/MM/yyyy");
            return new Models.Delegation.Delegant(délégant.IdDelegant, délégant.Nom, délégant.Adresse, dateContrat);
        }
    }
}
