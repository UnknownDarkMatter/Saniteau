using Saniteau.Facturation.Domain;
using Saniteau.Infrastructure.DataModel;
using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Saniteau.Facturation.Repository.Mappers
{
    public static class FacturationMapper
    {
        public static Domain.Facturation Map(FacturationModel model)
        {
            return new Domain.Facturation(IdFacturation.Parse(model.IdFacturation), model.IdCampagneFacturation, AbonnéMapper.Map(model.Abonné), model.DateFacturation,
                model.FacturationLignes.Select(FacturationLigneMapper.Map).ToList(), IdIndex.Parse(model.IdDernierIndex), model.Payée);
        }

        public static FacturationModel Map(Domain.Facturation facturation)
        {
            return new FacturationModel((int)facturation.IdFacturation, facturation.IdCampagneFacturation, (int)facturation.Abonné.IdAbonné, facturation.DateFacturation, 
                (int) facturation.IdDernierIndex, facturation.LignesFacturation.Select(FacturationLigneMapper.Map).ToList(), facturation.Payée);
        }
    }
}
