using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class AssocieAdressePDLCommand : IAction<Compteur>
    {
        public int IdAdresse { get; set; }
        public int IdCompteur { get; set; }

        public AssocieAdressePDLCommand(int idAdresse, int idCompteur)
        {
            IdAdresse = idAdresse;
            IdCompteur = idCompteur;
        }
    }
}
