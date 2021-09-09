using Saniteau.DSP.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class FacturePayeeAuDelegantMapper
    {
        public static FacturePayeeAuDelegantModel Map(FacturePayeeAuDelegant facturePayeeAuDelegant)
        {
            if (facturePayeeAuDelegant is null) { throw new ArgumentNullException(nameof(facturePayeeAuDelegant)); }

            return new FacturePayeeAuDelegantModel((int)facturePayeeAuDelegant.IdFacturePayeeAuDelegant, (int)facturePayeeAuDelegant.IdPayeDelegant,
                (int)facturePayeeAuDelegant.IdFacturation, (int)facturePayeeAuDelegant.IdAbonné);
        }

        public static FacturePayeeAuDelegant Map(FacturePayeeAuDelegantModel model)
        {
            if (model is null) { throw new ArgumentNullException(nameof(model)); }

            return new FacturePayeeAuDelegant(IdFacturePayeeAuDelegant.Parse(model.IdFacturePayeeAuDelegant), IdPayeDelegant.Parse(model.IdPayeDelegant),
                IdFacturation.Parse(model.IdFacturation), IdAbonné.Parse(model.IdAbonné));
        }
    }
}
