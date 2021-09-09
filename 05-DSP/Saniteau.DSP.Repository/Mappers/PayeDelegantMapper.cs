using Saniteau.DSP.Domain;
using Saniteau.Infrastructure.DataModel;
using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class PayeDelegantMapper
    {
        public static Domain.PayeDelegant Map(PayeDelegantModel model)
        {
            return new Domain.PayeDelegant(IdPayeDelegant.Parse(model.IdPayeDelegant), IdDelegant.Parse(model.IdDelegant), model.DatePaye.ToDate(),
                model.LignesPaye.Select(PayeDelegantLigneMapper.Map).ToList());
        }

        public static PayeDelegantModel Map(Domain.PayeDelegant payeDelegant)
        {
            return new PayeDelegantModel((int)payeDelegant.IdPayeDelegant, (int) payeDelegant.IdDelegant, payeDelegant.DatePaye.ToDateTime(0, 0, 0),
                payeDelegant.LignesPaye.Select(PayeDelegantLigneMapper.Map).ToList());
        }
    }
}
