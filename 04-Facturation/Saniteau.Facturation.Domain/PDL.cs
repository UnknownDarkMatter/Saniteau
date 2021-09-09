using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saniteau.Facturation.Domain
{
    public class PDL
    {
        public IdPDL IdPDL { get; private set; }
        public ChampLibre NuméroPDL { get; private set; }
        public PDL(IdPDL idPDL, ChampLibre numéroPDL)
        {
            if (idPDL is null) { throw new ArgumentNullException(nameof(idPDL)); }
            if (numéroPDL is null) { throw new ArgumentNullException(nameof(numéroPDL)); }

            IdPDL = idPDL;
            NuméroPDL = numéroPDL;
        }

        public void ChangeNuméroPDL(ChampLibre numéroPDL)
        {
            NuméroPDL = numéroPDL;
        }
    }
}
