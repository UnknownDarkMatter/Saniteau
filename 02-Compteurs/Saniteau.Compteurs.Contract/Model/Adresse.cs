using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Model
{
    public class Adresse
    {
        public int IdAdresse { get; set; }
        public string? NuméroEtRue { get; set; }
        public string? Ville { get; set; }
        public string? CodePostal { get; set; }

        public Adresse() { }

        public Adresse(int idAdresse, string? numéroEtRue, string? ville, string? codePostal)
        {
            IdAdresse = idAdresse;
            NuméroEtRue = numéroEtRue;
            Ville = ville;
            CodePostal = codePostal;
        }
    }
}
