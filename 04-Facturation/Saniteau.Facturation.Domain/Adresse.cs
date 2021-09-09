using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class Adresse
    {
        public IdAdresse IdAdresse { get; private set; }
        public ChampLibre NuméroEtRue { get; private set; }
        public ChampLibre Ville { get; private set; }
        public ChampLibre CodePostal { get; private set; }
        public Adresse(IdAdresse idAdresse, ChampLibre numéroEtRue, ChampLibre ville, ChampLibre codePostal)
        {
            if (idAdresse is null) { throw new ArgumentNullException(nameof(idAdresse)); }
            if (numéroEtRue is null) { throw new ArgumentNullException(nameof(numéroEtRue)); }
            if (ville is null) { throw new ArgumentNullException(nameof(ville)); }
            if (codePostal is null) { throw new ArgumentNullException(nameof(codePostal)); }

            IdAdresse = idAdresse;
            NuméroEtRue = numéroEtRue;
            Ville = ville;
            CodePostal = codePostal;
        }

    }
}
