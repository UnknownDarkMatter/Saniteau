using Saniteau.Compteurs.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Application.Mappers
{
    public class PDLMapper
    {
        public static Contract.Model.PDL Map(PDL pdl)
        {
            if (pdl is null) { return null; }

            var contractPDL = new Contract.Model.PDL((int)pdl.IdPDL, pdl.NuméroPDL.ToString());
            return contractPDL;
        }
        public static PDL Map(Contract.Model.PDL pdl)
        {
            if (pdl is null) { return null; }

            var domainPDL = new PDL(IdPDL.Parse(pdl.IdPDL), new ChampLibre(pdl.NuméroPDL));
            return domainPDL;
        }

    }
}
