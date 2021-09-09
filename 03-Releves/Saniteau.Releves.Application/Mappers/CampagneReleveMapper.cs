using Saniteau.Releves.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Application.Mappers
{
    public static class CampagneReleveMapper
    {
        public static Contract.Model.CampagneReleve Map(CampagneReleve campagneReleve)
        {
            var contract = new Contract.Model.CampagneReleve(campagneReleve.IdCampagneReleve, campagneReleve.DateCampagne, campagneReleve.ConsommationUsagersM3, campagneReleve.ConsommationPompePrincipaleM3);
            return contract;
        }
    }
}
