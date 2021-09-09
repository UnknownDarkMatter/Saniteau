using Saniteau.Compteurs.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public static class AbonnéMapper
    {
        public static Contract.Model.Abonne Map(Abonné abonné, Adresse adresse)
        {
            var tarification = TarificationMapper.Map(abonné.Tarification);
            return new Contract.Model.Abonne((int)abonné.IdAbonné, (int)abonné.IdAdresse, abonné.Nom.ToString(), abonné.Prénom.ToString(), tarification,
                adresse.NuméroEtRue.ToString(), adresse.Ville.ToString(), adresse.CodePostal.ToString());
        }

        public static Abonné Map(Contract.Model.Abonne model)
        {
            var tarification = TarificationMapper.Map(model.Tarification);
            return new Abonné(IdAbonné.Parse(model.IdAbonne), IdAdresse.Parse(model.IdAdresse), new ChampLibre(model.Nom), new ChampLibre(model.Prenom), tarification);
        }
    }
}
