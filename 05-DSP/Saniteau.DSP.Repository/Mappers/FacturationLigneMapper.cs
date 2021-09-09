using Saniteau.DSP.Domain;
using Saniteau.Infrastructure.DataModel;
using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class FacturationLigneMapper
    {
        public static FacturationLigne Map(FacturationLigneModel model)
        {
            return new FacturationLigne(IdFacturationLigne.Parse(model.IdFacturationLigne), IdFacturation.Parse(model.IdFacturation),
                ClasseLigneFacturationMapper.Map(model.ClasseLigneFacturation), Montant.FromDecimal(model.MontantEuros), model.ConsommationM3, Montant.FromDecimal(model.PrixM3));
        }
        public static FacturationLigneModel Map(FacturationLigne facturation)
        {
            return new FacturationLigneModel((int)facturation.IdFacturationLigne, (int)facturation.IdFacturation,
                ClasseLigneFacturationMapper.Map(facturation.Classe), (decimal)facturation.MontantEuros, facturation.ConsommationM3, (decimal)facturation.PrixM3);
        }
    }
}
