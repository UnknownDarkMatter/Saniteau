using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class FacturePayeeAuDelegant
    {
        public IdFacturePayeeAuDelegant IdFacturePayeeAuDelegant { get; private set; }
        public IdPayeDelegant IdPayeDelegant { get; private set; }
        public IdFacturation IdFacturation { get; private set; }
        public IdAbonné IdAbonné { get; private set; }

        public FacturePayeeAuDelegant(IdFacturePayeeAuDelegant idFacturePayeeAuDelegant, IdPayeDelegant idPayeDelegant, IdFacturation idFacturation, IdAbonné idAbonné)
        {
            if (idFacturePayeeAuDelegant is null) { throw new ArgumentNullException(nameof(idFacturePayeeAuDelegant)); }
            if (idPayeDelegant is null) { throw new ArgumentNullException(nameof(idPayeDelegant)); }
            if (idFacturation is null) { throw new ArgumentNullException(nameof(idFacturation)); }
            if (idAbonné is null) { throw new ArgumentNullException(nameof(idAbonné)); }

            IdFacturePayeeAuDelegant = idFacturePayeeAuDelegant;
            IdPayeDelegant = idPayeDelegant;
            IdFacturation = idFacturation;
            IdAbonné = idAbonné;
        }
    }
}
