using Saniteau.Common;
using Saniteau.Facturation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public static class FacturationLigneMapper
    {
        public static Contract.Model.FacturationLigne Map(FacturationLigne ligne)
        {
            var classe = ClasseLigneFacturationMapper.Map(ligne.Classe);
            return new Contract.Model.FacturationLigne((int)ligne.IdFacturationLigne, (int)ligne.IdFacturation, classe, (decimal)ligne.MontantEuros, 
                ligne.ConsommationM3, (decimal) ligne.PrixM3);
        }

        public static FacturationLigne Map(Contract.Model.FacturationLigne model)
        {
            var classe = ClasseLigneFacturationMapper.Map(model.ClasseLigneFacturation);
            return new FacturationLigne(IdFacturationLigne.Parse(model.IdFacturationLigne), 
                IdFacturation.Parse(model.IdFacturation), classe, Montant.FromDecimal(model.MontantEuros), 
                model.ConsommationM3, Montant.FromDecimal(model.PrixM3));
        }
    }
}
