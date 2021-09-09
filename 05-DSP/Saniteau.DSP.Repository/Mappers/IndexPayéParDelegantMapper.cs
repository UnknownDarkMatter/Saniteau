using Saniteau.DSP.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class IndexPayéParDelegantMapper
    {
        public static IndexPayéParDelegantModel Map(IndexPayéParDelegant indexPayéParDelegant)
        {
            if (indexPayéParDelegant is null) { throw new ArgumentNullException(nameof(indexPayéParDelegant)); }

            return new IndexPayéParDelegantModel((int)indexPayéParDelegant.IdIndexPayéParDelegant, (int)indexPayéParDelegant.IdPayeDelegant,
                (int)indexPayéParDelegant.IdCompteur, (int)indexPayéParDelegant.IdIndex);
        }

        public static IndexPayéParDelegant Map(IndexPayéParDelegantModel model)
        {
            if (model is null) { throw new ArgumentNullException(nameof(model)); }

            return new IndexPayéParDelegant(IdIndexPayéParDelegant.Parse(model.IdIndexPayéParDelegant), IdPayeDelegant.Parse(model.IdPayeDelegant),
                IdCompteur.Parse(model.IdCompteur), IdIndex.Parse(model.IdIndex));
        }

    }
}
