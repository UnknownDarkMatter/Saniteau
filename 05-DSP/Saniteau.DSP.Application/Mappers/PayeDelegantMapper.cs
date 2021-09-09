using Saniteau.Common;
using Saniteau.DSP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Application.Mappers
{
    public static class PayeDelegantMapper
    {
        public static PayeDelegant Map(Contract.Model.PayeDelegant model)
        {
            return new PayeDelegant(IdPayeDelegant.Parse(model.IdPayeDelegant), IdDelegant.Parse(model.IdDelegant), model.DatePaye, model.LignesPaye.Select(PayeDelegantLigneMapper.Map).ToList());
        }

        public static Contract.Model.PayeDelegant Map(PayeDelegant paye)
        {
            return new Contract.Model.PayeDelegant((int)paye.IdPayeDelegant, (int)paye.IdDelegant, paye.DatePaye, paye.LignesPaye.Select(PayeDelegantLigneMapper.Map).ToList());
        }
    }
}
