using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class PayeDelegant
    {
        public IdPayeDelegant IdPayeDelegant { get; private set; }
        public IdDelegant IdDelegant { get; private set; }
        public Date DatePaye { get; private set; }
        public List<PayeDelegantLigne> LignesPaye { get; private set; }

        public PayeDelegant(IdPayeDelegant idPayeDelegant, IdDelegant idDelegant, Date datePaye, List<PayeDelegantLigne> lignesPaye)
        {
            if (idPayeDelegant is null) { throw new ArgumentNullException(nameof(idPayeDelegant)); }
            if (idDelegant is null) { throw new ArgumentNullException(nameof(idDelegant)); }
            if (datePaye is null) { throw new ArgumentNullException(nameof(datePaye)); }
            if (lignesPaye is null) { throw new ArgumentNullException(nameof(lignesPaye)); }

            IdPayeDelegant = idPayeDelegant;
            IdDelegant = idDelegant;
            DatePaye = datePaye;
            LignesPaye = lignesPaye;
        }

        public void ChangeDatePaye(Date datePaye) {
            DatePaye = datePaye;
        }
    }
}
