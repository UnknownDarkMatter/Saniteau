using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class PoseCompteurCommand : IAction
    {
        public Compteur Compteur { get; set; }

        public PoseCompteurCommand(Compteur compteur)
        {
            Compteur = compteur;
        }
    }
}
