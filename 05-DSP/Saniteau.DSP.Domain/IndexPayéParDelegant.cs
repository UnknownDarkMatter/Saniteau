using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class IndexPayéParDelegant
    {
        public IdIndexPayéParDelegant IdIndexPayéParDelegant { get; private set; }
        public IdPayeDelegant IdPayeDelegant { get; private set; }
        public IdCompteur IdCompteur { get; private set; }
        public IdIndex IdIndex { get; private set; }

        public IndexPayéParDelegant(IdIndexPayéParDelegant idIndexPayéParDelegant, IdPayeDelegant idPayeDelegant, IdCompteur idCompteur, IdIndex idIndex)
        {
            if (idIndexPayéParDelegant is null) { throw new ArgumentNullException(nameof(idIndexPayéParDelegant)); }
            if (idPayeDelegant is null) { throw new ArgumentNullException(nameof(idPayeDelegant)); }
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }
            if (idIndex is null) { throw new ArgumentNullException(nameof(idIndex)); }

            IdIndexPayéParDelegant = idIndexPayéParDelegant;
            IdPayeDelegant = idPayeDelegant;
            IdCompteur = idCompteur;
            IdIndex = idIndex;
        }
    }
}
