using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain.Commands
{
    public class CréeDelegantDomainCommand
    {
        public ChampLibre Nom { get; private set; }
        public ChampLibre Adresse { get; private set; }
        public Date DateContrat { get; private set; }

        public CréeDelegantDomainCommand(ChampLibre nom, ChampLibre adresse, Date dateContrat)
        {
            if (nom is null) { throw new ArgumentNullException(nameof(nom)); }
            if (adresse is null) { throw new ArgumentNullException(nameof(nom)); }
            if (dateContrat is null) { throw new ArgumentNullException(nameof(nom)); }

            Nom = nom;
            Adresse = adresse;
            DateContrat = dateContrat;
        }

    }
}
