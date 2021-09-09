using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Releves.Domain.Commands
{
    public class GetAllCampagnesReleveDomainCommand
    {
        public List<CampagneReleve> GetAllCampagnesReleve(RéférentielPompes référentielPompes, RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielCompteurs référentielCompteurs)
        {
            List<CampagneReleve> result = new List<CampagneReleve>();
            var pompePrincipale = référentielPompes.GetAllPompes().FirstOrDefault();
            if(pompePrincipale is null) { throw new BusinessException($"Un relevé nécessite une pompe principale"); }

            var compteursWithIndexes = référentielCompteurs.GetAllCompteursIndexes();
            foreach (var compteursIndexes in compteursWithIndexes.GroupBy(m => m.Item2.IdCampagneReleve).Select(m => new { IdCampagne = m.Key, CompteurIndex = m.ToList() }).OrderBy(m => m.IdCampagne))
            {
                int idCampagneReleve = compteursIndexes.IdCampagne;
                DateTime dateCampagne = DateTime.MinValue;
                int consommationUsagersM3 = 0;
                int consommationPompePrincipaleM3 = 0;
                foreach (var compteurIndex in compteursIndexes.CompteurIndex)
                {
                    if (compteurIndex.Item1.IdCompteur == pompePrincipale.IdCompteur)
                    {
                        consommationPompePrincipaleM3 += compteurIndex.Item2.IndexM3;
                        dateCampagne = compteurIndex.Item2.DateIndex;
                    }
                    else
                    {
                        consommationUsagersM3 += compteurIndex.Item2.IndexM3;
                    }
                }
                var campagne = new CampagneReleve(idCampagneReleve, dateCampagne, consommationUsagersM3, consommationPompePrincipaleM3);
                result.Add(campagne);
            }
            return result;
        }
    }
}
