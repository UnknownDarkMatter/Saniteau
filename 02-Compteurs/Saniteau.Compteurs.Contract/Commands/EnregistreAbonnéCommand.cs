using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class EnregistreAbonnéCommand : IAction<Abonne>
    {
        public int IdAbonné { get; set; }
        public int IdAdresse { get; set; }
        public string? Nom { get; set; }
        public string? Prénom { get; set; }
        public Tarification Tarification { get; set; }

        public EnregistreAbonnéCommand(int idAbonné, int idAdresse, string? nom, string? prénom, Tarification tarification)
        {
            IdAbonné = idAbonné;
            IdAdresse = idAdresse;
            Nom = nom;
            Prénom = prénom;
            Tarification = tarification;
        }
    }
}
