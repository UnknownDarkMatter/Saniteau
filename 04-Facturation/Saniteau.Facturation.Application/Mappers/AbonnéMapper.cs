using Saniteau.Facturation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public static class AbonnéMapper
    {
        public static Contract.Model.Abonné Map(Abonné abonné, Adresse adresse)
        {
            var tarification = TarificationMapper.Map(abonné.Tarification);
            return new Contract.Model.Abonné((int)abonné.IdAbonné, (int)abonné.IdAdresse, abonné.Nom.ToString(), abonné.Prénom.ToString(), tarification,
                adresse.NuméroEtRue.ToString(), adresse.Ville.ToString(), adresse.CodePostal.ToString());
        }

        public static Abonné Map(Contract.Model.Abonné model)
        {
            var tarification = TarificationMapper.Map(model.Tarification);
            return new Abonné(IdAbonné.Parse(model.IdAbonné), IdAdresse.Parse(model.IdAdresse), new ChampLibre(model.Nom), new ChampLibre(model.Prénom), tarification);
        }
    }
}
