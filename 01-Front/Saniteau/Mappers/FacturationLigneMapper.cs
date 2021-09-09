using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Mappers
{
    public static class FacturationLigneMapper
    {
        public static Models.Facturation.FacturationLigne Map(Facturation.Contract.Model.FacturationLigne ligne)
        {
            var classe = ClasseLigneFacturationMapper.Map(ligne.ClasseLigneFacturation);
            return new Models.Facturation.FacturationLigne((int)ligne.IdFacturationLigne, (int)ligne.IdFacturation, classe, (decimal)ligne.MontantEuros, 
                ligne.ConsommationM3, (decimal) ligne.PrixM3);
        }

        public static Facturation.Contract.Model.FacturationLigne Map(Models.Facturation.FacturationLigne model)
        {
            var classe = ClasseLigneFacturationMapper.Map(model.ClasseLigneFacturation);
            return new Facturation.Contract.Model.FacturationLigne(model.IdFacturationLigne,
                model.IdFacturation, classe, model.MontantEuros, 
                model.ConsommationM3, model.PrixM3);
        }
    }
}
