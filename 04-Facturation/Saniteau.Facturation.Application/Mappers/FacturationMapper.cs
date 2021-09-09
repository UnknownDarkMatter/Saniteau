using Saniteau.Facturation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public static class FacturationMapper
    {
        public static Contract.Model.Facturation Map(Domain.Facturation facturation, Domain.Adresse adresse)
        {
            var abonné = AbonnéMapper.Map(facturation.Abonné, adresse);
            return new Contract.Model.Facturation((int)facturation.IdFacturation, facturation.IdCampagneFacturation, abonné, facturation.DateFacturation, 
                facturation.LignesFacturation.Select(FacturationLigneMapper.Map).ToList(), (int)facturation.IdDernierIndex, facturation.Payée);
        }

    }
}
