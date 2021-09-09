using Saniteau.Facturation.Domain;
using Saniteau.Infrastructure.DataModel;
using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Repository.Mappers
{
    public static class AppairageCompteurMapper
    {
        public static AppairageCompteurModel Map(AppairageCompteur appairage)
        {
            if (appairage is null) { return null; }

            var model = new AppairageCompteurModel((int)appairage.IdAppairageCompteur ,(int)appairage.IdPDL, (int)appairage.IdCompteur, 
                appairage.DateAppairage?.ToDateTime(0, 0, 0), appairage.DateDésappairage?.ToDateTime(0, 0, 0));
            return model;
        }

        public static AppairageCompteur Map(AppairageCompteurModel model)
        {
            if (model is null) { return null; }

            var appairage = new AppairageCompteur(IdAppairageCompteur.Parse(model.IdAppairageCompteur),  IdPDL.Parse(model.IdPDL), IdCompteur.Parse(model.IdCompteur ?? 0),
                model.DateAppairage?.ToDate(), model.DateDésappairage?.ToDate());
            return appairage;
        }

    }
}
