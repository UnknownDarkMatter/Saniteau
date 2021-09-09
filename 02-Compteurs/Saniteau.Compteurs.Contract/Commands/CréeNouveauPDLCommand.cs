using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class CréeNouveauPDLCommand : IAction<PDL>
    {
        public string NuméroPDL { get; set; }

        public CréeNouveauPDLCommand(string numéroPDL)
        {
            NuméroPDL = numéroPDL;
        }

    }
}
