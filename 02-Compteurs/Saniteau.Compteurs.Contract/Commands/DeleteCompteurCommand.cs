using Saniteau.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class DeleteCompteurCommand : IAction
    {
        public int IdCompteur { get; set; }

        public DeleteCompteurCommand(int idCompteur)
        {
            IdCompteur = idCompteur;
        }
    }
}
