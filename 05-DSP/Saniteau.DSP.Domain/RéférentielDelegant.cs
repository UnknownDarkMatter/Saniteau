using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public interface RéférentielDelegant
    {
        List<Delegant> GetDelegants();
        Delegant EnregistreDelegant(Delegant delegant);
    }
}
