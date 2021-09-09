using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public interface RéférentielPaye
    {
        PayeDelegant EnregistrePayeDelegant(PayeDelegant payeDelegant);
        List<PayeDelegant> GetPayesDelegants();
        List<FacturePayeeAuDelegant> GetFacturesPayeesAuDelegants();
        FacturePayeeAuDelegant EnregistreFacturePayeeAuDelegant(FacturePayeeAuDelegant facturePayeeAuDelegant);
        List<IndexPayéParDelegant> GetIndexesPayesParDelegants();
        IndexPayéParDelegant EnregistreIndexesPayésParDelegant(IndexPayéParDelegant indexesPayésParDelegant);
    }
}
