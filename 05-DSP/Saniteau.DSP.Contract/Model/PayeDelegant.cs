using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Contract.Model
{
    public class PayeDelegant
    {
        public int IdPayeDelegant { get; set; }
        public int IdDelegant { get; set; }
        public Date DatePaye { get; set; }
        public List<PayeDelegantLigne> LignesPaye { get; set; }

        public PayeDelegant() { }

        public PayeDelegant(int idPayeDelegant, int idDelegant, Date datePaye, List<PayeDelegantLigne> lignesPaye)
        {
            if (datePaye is null) { throw new ArgumentNullException(nameof(datePaye)); }
            if (lignesPaye is null) { throw new ArgumentNullException(nameof(lignesPaye)); }

            IdPayeDelegant = idPayeDelegant;
            IdDelegant = idDelegant;
            DatePaye = datePaye;
            LignesPaye = lignesPaye;
        }
    }
}
