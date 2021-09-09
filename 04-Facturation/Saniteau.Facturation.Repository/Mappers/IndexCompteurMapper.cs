using Saniteau.Common;
using Saniteau.Facturation.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Repository.Mappers
{
    public static class IndexCompteurMapper
    {
        public static IndexCompteurModel Map(IndexCompteur index)
        {
            if (index is null) { return null; }

            return new IndexCompteurModel((int)index.IdIndex, (int)index.IdCompteur, index.IdCampagneReleve, index.IndexM3, index.DateIndex);
        }

        public static IndexCompteur Map(IndexCompteurModel model)
        {
            if (model is null) { return null; }

            return new IndexCompteur(IdIndex.Parse(model.IdIndex), IdCompteur.Parse(model.IdCompteur), model.IdCampagneReleve, model.IndexM3, model.DateIndex);
        }
    }
}
