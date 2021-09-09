using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Mappers
{
    public static class PDLMapper
    {
        public static Models.Compteurs.PDL Map(Compteurs.Contract.Model.PDL pdl)
        {
            if (pdl == null) { return null; }

            return new Models.Compteurs.PDL(pdl.IdPDL, pdl.NuméroPDL);
        }
        public static Compteurs.Contract.Model.PDL Map(Models.Compteurs.PDL pdl)
        {
            if (pdl == null) { return null; }

            return new Compteurs.Contract.Model.PDL(pdl.IdPDL, pdl.NumeroPDL);
        }

    }
}
