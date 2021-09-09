using Saniteau.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class DeleteAbonnéCommand : IAction
    {
        public int IdAbonné { get; set; }
        public DeleteAbonnéCommand(int idAbonné)
        {
            IdAbonné = idAbonné;
        }
    }
}
