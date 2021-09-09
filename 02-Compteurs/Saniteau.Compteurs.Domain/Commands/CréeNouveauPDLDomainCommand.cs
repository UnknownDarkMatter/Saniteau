using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class CréeNouveauPDLDomainCommand
    {
        public ChampLibre NuméroPDL { get; private set; }

        public CréeNouveauPDLDomainCommand(ChampLibre numéroPDL)
        {
            if (numéroPDL is null) { throw new ArgumentNullException(nameof(numéroPDL)); }

            NuméroPDL = numéroPDL;
        }

        public PDL CréeNouveauPDL(RéférentielPDL référentielPDL)
        {
            if (référentielPDL is null) { throw new ArgumentNullException(nameof(référentielPDL)); }

            return référentielPDL.CréeNouveauPDL(new PDL(IdPDL.Parse(0), NuméroPDL));
        }

    }
}
