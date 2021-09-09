using Saniteau.Common;
using Saniteau.DSP.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class DelegantMapper
    {
        public static DelegantModel Map(Delegant delegant)
        {
            if (delegant is null) { throw new ArgumentNullException(nameof(delegant)); }

            return new DelegantModel((int)delegant.IdDelegant, delegant.Nom.ToString(), delegant.Adresse.ToString(), delegant.DateContrat.ToDateTime(0, 0, 0) );
        }

        public static Delegant Map(DelegantModel model)
        {
            if (model is null) { throw new ArgumentNullException(nameof(model)); }

            return new Delegant(IdDelegant.Parse(model.IdDelegant), new ChampLibre(model.Nom), new ChampLibre(model.Adresse), model.DateContrat.ToDate());
        }

    }
}
