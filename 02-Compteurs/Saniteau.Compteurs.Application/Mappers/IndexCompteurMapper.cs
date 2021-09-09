using Saniteau.Compteurs.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public static class IndexCompteurMapper
    {
        public static Contract.Model.IndexCompteur Map(IndexCompteur index)
        {
            return new Contract.Model.IndexCompteur((int)index.IdIndex, (int)index.IdCompteur, index.IdCampagneReleve, index.IndexM3, index.DateIndex);
        }

        public static IndexCompteur Map(Contract.Model.IndexCompteur model)
        {
            return new IndexCompteur(IdIndex.Parse(model.IdIndex), IdCompteur.Parse(model.IdCompteur), model.IndexM3, model.IdCampagneReleve, model.DateIndex);
        }

    }
}
