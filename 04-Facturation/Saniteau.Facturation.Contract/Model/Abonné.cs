using Saniteau.Facturation.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Contract.Model
{
    public class Abonné
    {
        public int IdAbonné { get; set; }
        public int IdAdresse { get; set; }
        public string? Nom { get; set; }
        public string? Prénom { get; set; }
        public string? NumeroEtRue { get; set; }
        public string? Ville { get; set; }
        public string? CodePostal { get; set; }
        public Tarification Tarification  { get; set; }

        public Abonné()
        {

        }

        public Abonné(int idAbonne, int idAdresse, string? nom, string? prenom, Tarification tarification, string? numeroEtRue, string? ville, string? codePostal)
        {
            IdAbonné = idAbonne;
            IdAdresse = idAdresse;
            Nom = nom;
            Prénom = prenom;
            Tarification = tarification;
            NumeroEtRue = numeroEtRue;
            Ville = ville;
            CodePostal = codePostal;
        }

    }
}
