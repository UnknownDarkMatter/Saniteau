using Saniteau.Common;
using Saniteau.DSP.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Mappers
{
    public static class PayeDelegantLigneMapper
    {
        public static PayeDelegantLigne Map(Contract.Model.PayeDelegantLigne model)
        {
            return new PayeDelegantLigne(IdPayeDelegantLigne.Parse(model.IdPayeDelegantLigne), IdPayeDelegant.Parse(model.IdPayeDelegant),
                ClasseLignePayeDelegantMapper.Map(model.Classe), Montant.FromDecimal(model.MontantEuros));
        }

        public static Contract.Model.PayeDelegantLigne Map(PayeDelegantLigne ligne)
        {
            return new Contract.Model.PayeDelegantLigne((int) ligne.IdPayeDelegantLigne, (int)ligne.IdPayeDelegant, ClasseLignePayeDelegantMapper.Map(ligne.Classe), (decimal) ligne.MontantEuros);
        }
    }
}
