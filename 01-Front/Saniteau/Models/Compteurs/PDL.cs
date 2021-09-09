using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Models.Compteurs
{
    public class PDL
    {
        public int IdPDL { get; set; }
        public string NumeroPDL { get; set; }

        public PDL()
        {

        }
        public PDL(int idPDL, string numeroPDL)
        {
            IdPDL = idPDL;
            NumeroPDL = numeroPDL;
        }

    }
}
