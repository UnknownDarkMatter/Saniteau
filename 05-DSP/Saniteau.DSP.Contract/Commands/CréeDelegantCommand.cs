using Saniteau.Common;
using Saniteau.Common.Mediator;
using Saniteau.DSP.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Contract.Commands
{
    public class CréeDelegantCommand : IAction<Delegant>
    {
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public Date DateContrat { get; set; }

        public CréeDelegantCommand(string nom, string adresse, Date dateContrat)
        {
            Nom = nom;
            Adresse = adresse;
            DateContrat = dateContrat;
        }

    }
}
