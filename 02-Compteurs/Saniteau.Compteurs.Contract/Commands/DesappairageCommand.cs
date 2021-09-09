using Saniteau.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class DesappairageCommand : IAction
    {
        public int IdAppairageCompteur { get; set; }

        public DesappairageCommand(int idAppairageCompteur)
        {
            IdAppairageCompteur = idAppairageCompteur;
        }
    }
}
