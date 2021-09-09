using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class EnregistreAdresseCommand : IAction<Adresse>
    {
        public int IdAdresse { get; set; }
        public string? NuméroEtRue { get; set; }
        public string? Ville { get; set; }
        public string? CodePostal { get; set; }

        public EnregistreAdresseCommand(int idAdresse, string? numéroEtRue, string? ville, string? codePostal)
        {
            IdAdresse = idAdresse;
            NuméroEtRue = numéroEtRue;
            Ville = ville;
            CodePostal = codePostal;
        }

    }
}
