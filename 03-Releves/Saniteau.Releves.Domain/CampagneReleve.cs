using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Domain
{
    public class CampagneReleve
    {
        public int IdCampagneReleve { get; private set; }
        public DateTime DateCampagne { get; private set; }
        public int ConsommationUsagersM3 { get; private set; }
        public int ConsommationPompePrincipaleM3 { get; private set; }

        public CampagneReleve(int idCampagneReleve, DateTime dateCampagne, int consommationUsagersM3, int consommationPompePrincipaleM3)
        {
            IdCampagneReleve = idCampagneReleve;
            DateCampagne = dateCampagne;
            ConsommationUsagersM3 = consommationUsagersM3;
            ConsommationPompePrincipaleM3 = consommationPompePrincipaleM3;
        }
    }
}
