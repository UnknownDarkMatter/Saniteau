using Saniteau.DSP.Domain;
using Saniteau.DSP.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Repository
{
    public class RéférentielCompteursOnEfCore : RéférentielCompteurs
    {
        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielCompteursOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public List<Compteur> GetAllCompteurs()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Compteurs.Select(CompteurMapper.Map).ToList();
            }
        }
    }
}
