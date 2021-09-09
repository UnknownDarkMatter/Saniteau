using Saniteau.Facturation.Domain;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Repository.Mappers
{
    public static class AdresseMapper
    {
        public static AdresseModel Map(Adresse adresse)
        {
            if(adresse is null) { return null; }

            return new AdresseModel((int)adresse.IdAdresse, adresse.NuméroEtRue.ToString(), adresse.Ville.ToString(), adresse.CodePostal.ToString());
        }

        public static Adresse Map(AdresseModel model)
        {
            if (model is null) { return null; }

            return new Adresse(IdAdresse.Parse(model.IdAdresse), new ChampLibre(model.NuméroEtRue), new ChampLibre(model.Ville), new ChampLibre(model.CodePostal)); 
        }
    }
}
