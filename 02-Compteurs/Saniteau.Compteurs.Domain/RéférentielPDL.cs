using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain
{
    public interface RéférentielPDL
    {
        PDL CréeNouveauPDL(PDL pdl);
        PDL GetPDL(IdPDL idPDL);
        void SupprimePDL(IdPDL idPDL);
    }
}
