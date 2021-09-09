using Saniteau.Common.Mediator;
using Saniteau.Releves.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Contract.Commands
{
    public class CréePompeCommand : IAction<Pompe>
    {
        public int IdCompteur { get; set; }
        public string NuméroPompe { get; set; }

        public CréePompeCommand(int idCompteur, string numéroPompe)
        {
            IdCompteur = idCompteur;
            NuméroPompe = numéroPompe;
        }
    }
}
