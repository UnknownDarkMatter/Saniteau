using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class EnregistreCompteurCommand : IAction<Compteur>
    {
        public int IdCompteur { get; set; }
        public string NuméroCompteur { get; set; }

        public EnregistreCompteurCommand(int idCompteur, string numéroCompteur)
        {
            IdCompteur = idCompteur;
            NuméroCompteur = numéroCompteur;
        }
    }
}
