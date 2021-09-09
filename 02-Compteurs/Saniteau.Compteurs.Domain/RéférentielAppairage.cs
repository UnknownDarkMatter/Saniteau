using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain
{
    public interface RéférentielAppairage
    {
        AppairageCompteur EnregistreAppairageCompteur(AppairageCompteur appairageCompteur);

        List<AppairageCompteur> GetAppairageOfPDL(IdPDL idPDL);
        AppairageCompteur GetAppairageOfPDL(IdAppairageCompteur idAppairageCompteur);

        AdressePDL CréeAdressePDL(AdressePDL adressePDL);

        void SupprimeAdressePDL(IdAdresse idAdresse);

        List<AdressePDL> GetAdressesPDL(IdAdresse idAdresse);
        List<AdressePDL> GetAdressesPDL(IdPDL idPDL);
        void  SupprimeAppairagge(AppairageCompteur appairageCompteur);

    }
}
