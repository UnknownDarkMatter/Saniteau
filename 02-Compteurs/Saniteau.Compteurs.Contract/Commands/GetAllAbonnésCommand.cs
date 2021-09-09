using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class GetAllAbonnésCommand : IAction<List<Abonne>>
    {
        public bool FiltrerAbonnésAvecCompteur { get; set; }

        public GetAllAbonnésCommand(bool filtrerAbonnésAvecCompteur) {
            FiltrerAbonnésAvecCompteur = filtrerAbonnésAvecCompteur;
        }
    }
}
