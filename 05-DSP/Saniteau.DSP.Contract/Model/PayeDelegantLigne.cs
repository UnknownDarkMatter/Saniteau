using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Contract.Model
{
    public class PayeDelegantLigne
    {
        public int IdPayeDelegantLigne { get; private set; }
        public int IdPayeDelegant { get; private set; }
        public ClasseLignePayeDelegant Classe { get; private set; }
        public decimal MontantEuros { get; private set; }

        public PayeDelegantLigne() { }

        public PayeDelegantLigne(int idPayeDelegantLigne, int idPayeDelegant, ClasseLignePayeDelegant classe, decimal montantEuros)
        {
            IdPayeDelegantLigne = idPayeDelegantLigne;
            IdPayeDelegant = idPayeDelegant;
            Classe = classe;
            MontantEuros = montantEuros;
        }
    }
}
