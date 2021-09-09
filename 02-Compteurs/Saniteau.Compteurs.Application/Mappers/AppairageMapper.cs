using Saniteau.Compteurs.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public static class AppairageMapper
    {
        public static Contract.Model.AppairageCompteur Map(AppairageCompteur appairageCompteur, IdAdresse idAdresse)
        {
            DateTime? dateAppairage = null;
            if(appairageCompteur.DateAppairage != null)
            {
                dateAppairage = appairageCompteur.DateAppairage.ToDateTime(0, 0, 0);
            }
            DateTime? dateDésappairage = null;
            if (appairageCompteur.DateDésappairage != null)
            {
                dateDésappairage = appairageCompteur.DateDésappairage.ToDateTime(0, 0, 0);
            }
            var contractAppairage = new Contract.Model.AppairageCompteur((int)appairageCompteur.IdAppairageCompteur, (int)appairageCompteur.IdPDL, (int)appairageCompteur.IdCompteur, (int)idAdresse, dateAppairage, dateDésappairage);
            return contractAppairage;
        }
    }
}
