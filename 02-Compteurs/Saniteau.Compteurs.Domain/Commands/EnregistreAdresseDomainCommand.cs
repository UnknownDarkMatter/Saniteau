using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class EnregistreAdresseDomainCommand
    {
        public IdAdresse IdAdresse { get; private set; }
        public ChampLibre NuméroEtRue { get; private set; }
        public ChampLibre Ville { get; private set; }
        public ChampLibre CodePostal { get; private set; }
        public EnregistreAdresseDomainCommand(IdAdresse idAdresse, ChampLibre numéroEtRue, ChampLibre ville, ChampLibre codePostal)
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


        public Adresse EnregistreAdresse(RéférentielAbonnés référentielAbonnés)
        {
            if (référentielAbonnés is null) { throw new ArgumentNullException(nameof(référentielAbonnés)); }

            return référentielAbonnés.EnregistreAdresse(new Adresse(IdAdresse, NuméroEtRue, Ville, CodePostal));
        }
    }
}
