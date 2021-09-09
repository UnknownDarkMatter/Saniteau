using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public interface RéférentielPDL
    {
       PDL GetPDL(IdPDL idPDL);
    }
}
