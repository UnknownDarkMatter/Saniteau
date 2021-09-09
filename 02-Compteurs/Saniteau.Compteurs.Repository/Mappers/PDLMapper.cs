using Saniteau.Compteurs.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Repository.Mappers
{
    public static class PDLMapper
    {
        public static PDLModel Map(PDL pdl)
        {
            if (pdl is null) { return null; }

            var model = new PDLModel((int)pdl.IdPDL, pdl.NuméroPDL?.ToString());
            return model;
        }

        public static PDL Map(PDLModel model)
        {
            if (model is null) { return null; }

            var entity = new PDL(IdPDL.Parse(model.IdPDL), new ChampLibre(model.NuméroPDL));
            return entity;
        }
    }
}
