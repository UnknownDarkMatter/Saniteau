using Saniteau.Compteurs.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public static class AdresseMapper
    {
        public static Contract.Model.Adresse Map(Adresse adresse)
        {
            return new Contract.Model.Adresse((int)adresse.IdAdresse, adresse.NuméroEtRue.ToString(), adresse.Ville.ToString(), adresse.CodePostal.ToString());
        }

        public static Adresse Map(Contract.Model.Adresse model)
        {
            return new Adresse(IdAdresse.Parse(model.IdAdresse), new ChampLibre(model.NuméroEtRue), new ChampLibre(model.Ville), new ChampLibre(model.CodePostal));
        }
    }
}
