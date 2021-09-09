using Saniteau.DSP.Domain;
using Saniteau.DSP.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Repository
{
    public class RéférentielIndexesCompteursOnEfCore : RéférentielIndexesCompteurs
    {
        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielIndexesCompteursOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }


        public List<IndexCompteur> GetIndexesOfCompteur(IdCompteur idCompteur)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.IndexesCompteur.Where(i => i.IdCompteur == (int)idCompteur).Select(IndexCompteurMapper.Map).ToList();
            }
        }
    }
}
