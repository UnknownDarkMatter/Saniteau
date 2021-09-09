using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class AdressePDL
    {
        public IdAdressePDL IdAdressePDL { get; private set; }
        public IdAdresse IdAdresse { get; private set; }
        public IdPDL IdPDL { get; private set; }
        public AdressePDL(IdAdressePDL idAdressePDL, IdAdresse idAdresse, IdPDL idPDL)
        {
            if (idAdressePDL is null) { throw new ArgumentNullException(nameof(idAdressePDL)); }
            if (idAdresse is null) { throw new ArgumentNullException(nameof(idAdresse)); }
            if (idPDL is null) { throw new ArgumentNullException(nameof(idPDL)); }

            IdAdressePDL = idAdressePDL;
            IdAdresse = idAdresse;
            IdPDL = idPDL;
        }
    }
}
