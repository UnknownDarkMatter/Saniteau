﻿using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class GetCompteurCommand : IAction<Compteur>
    {
        public int IdCompteur { get; set; }
        public GetCompteurCommand(int idCompteur) {
            IdCompteur = idCompteur;
        }
    }
}
