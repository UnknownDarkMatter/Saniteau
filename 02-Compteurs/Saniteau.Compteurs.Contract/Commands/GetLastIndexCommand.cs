﻿using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class GetLastIndexCommand : IAction<IndexCompteur>
    {
        public int IdCompteur { get; set; }

        public GetLastIndexCommand(int idCompteur)
        {
            IdCompteur = idCompteur;
        }
    }
}
