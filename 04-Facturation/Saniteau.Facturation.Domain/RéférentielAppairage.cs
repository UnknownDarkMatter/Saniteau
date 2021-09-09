using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public interface RéférentielAppairage
    {
        List<AppairageCompteur> GetAppairageOfPDL(IdPDL idPDL);

        List<AdressePDL> GetAdressesPDL(IdAdresse idAdresse);
    }
}
