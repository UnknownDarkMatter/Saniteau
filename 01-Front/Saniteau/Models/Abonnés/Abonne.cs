using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Models.Abonnés
{
    public class Abonne
    {
        public int IdAbonne { get; set; }
        public int IdAdresse { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? NumeroEtRue { get; set; }
        public string? Ville { get; set; }
        public string? CodePostal { get; set; }
        public Tarification Tarification  { get; set; }

        public Abonne()
        {

        }

        public Abonne(int idAbonne , int idAdresse, string? nom, string? prenom, Tarification tarification, string? numeroEtRue, string? ville, string? codePostal)
        {
            IdAbonne = idAbonne;
            IdAdresse = idAdresse;
            Nom = nom;
            Prenom = prenom;
            Tarification = tarification;
            NumeroEtRue = numeroEtRue;
            Ville = ville;
            CodePostal = codePostal;
        }

    }
}
