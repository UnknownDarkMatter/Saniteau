using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Saniteau.Mappers
{
    public static class AbonnéMapper
    {
        public static Models.Abonnés.Abonne Map(Facturation.Contract.Model.Abonné abonné)
        {
            return new Models.Abonnés.Abonne(abonné.IdAbonné, abonné.IdAdresse, abonné.Nom, abonné.Prénom, TarificationMapper.Map(abonné.Tarification),
                abonné.NumeroEtRue, abonné.Ville, abonné.CodePostal);
        }
        public static Facturation.Contract.Model.Abonné MapToFacturation(Models.Abonnés.Abonne abonné)
        {
            return new Facturation.Contract.Model.Abonné(abonné.IdAbonne, abonné.IdAdresse, abonné.Nom, abonné.Prenom, TarificationMapper.MapToFacturation(abonné.Tarification),
                abonné.NumeroEtRue, abonné.Ville, abonné.CodePostal);
        }

    }
}
