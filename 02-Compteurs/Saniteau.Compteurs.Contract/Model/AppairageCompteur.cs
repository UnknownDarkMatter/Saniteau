using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Model
{
    public class AppairageCompteur
    {
        public int IdAppairageCompteur { get; set; }
        public int IdPDL { get; set; }
        public int IdCompteur { get; set; }
        public int IdAdresse { get; set; }
        public DateTime? DateAppairage { get; set; }
        public DateTime? DateDésappairage { get; set; }

        public AppairageCompteur() { }

        public AppairageCompteur(int idAppairageCompteur, int idPDL, int idCompteur, int idAdresse, DateTime? dateAppairage, DateTime? dateDésappairage)
        {
            IdAppairageCompteur = idAppairageCompteur;
            IdPDL = idPDL;
            IdCompteur = idCompteur;
            IdAdresse = idAdresse;
            DateAppairage = dateAppairage;
            DateDésappairage = dateDésappairage;
        }

    }
}
