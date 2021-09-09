using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Model
{
    public class PDL
    {
        public int IdPDL { get; set; }
        public string NuméroPDL { get; set; }

        public PDL()
        {

        }
        public PDL(int idPDL, string numéroPDL)
        {
            IdPDL = idPDL;
            NuméroPDL = numéroPDL;
        }

    }
}
