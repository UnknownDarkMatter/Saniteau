using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class GetAbonnéCommand : IAction<Abonne>
    {
        public int IdAbonné { get; set; }

        public GetAbonnéCommand(int idAbonné) {
            IdAbonné = idAbonné;
        }
    }
}
