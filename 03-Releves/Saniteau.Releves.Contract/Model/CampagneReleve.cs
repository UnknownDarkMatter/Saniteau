using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Contract.Model
{
    public class CampagneReleve
    {
        public int IdCampagneReleve { get; set; }
        public DateTime DateCampagne { get; set; }
        public int ConsommationUsagersM3 { get; set; }
        public int ConsommationPompePrincipaleM3 { get; set; }

        public CampagneReleve(int idCampagneReleve, DateTime dateCampagne, int consommationUsagersM3, int consommationPompePrincipaleM3)
        {
            IdCampagneReleve = idCampagneReleve;
            DateCampagne = dateCampagne;
            ConsommationUsagersM3 = consommationUsagersM3;
            ConsommationPompePrincipaleM3 = consommationPompePrincipaleM3;
        }
    }
}
