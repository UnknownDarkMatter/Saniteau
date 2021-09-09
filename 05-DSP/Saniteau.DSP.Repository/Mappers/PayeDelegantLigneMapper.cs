using Saniteau.Common;
using Saniteau.DSP.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class PayeDelegantLigneMapper
    {
        public static PayeDelegantLigne Map(PayeDelegantLigneModel model)
        {
            return new PayeDelegantLigne(IdPayeDelegantLigne.Parse(model.IdPayeDelegantLigne), IdPayeDelegant.Parse(model.IdPayeDelegant),
                ClasseLignePayeDelegantMapper.Map(model.Classe), Montant.FromDecimal(model.MontantEuros));
        }
        public static PayeDelegantLigneModel Map(PayeDelegantLigne payeDelegantLigne)
        {
            return new PayeDelegantLigneModel((int)payeDelegantLigne.IdPayeDelegantLigne, (int)payeDelegantLigne.IdPayeDelegant,
                ClasseLignePayeDelegantMapper.Map(payeDelegantLigne.Classe), (decimal)payeDelegantLigne.MontantEuros);
        }

    }
}
