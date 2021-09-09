using Saniteau.DSP.Domain;
using Saniteau.Infrastructure.DataContext;
using Saniteau.DSP.Repository.Mappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Saniteau.DSP.Repository
{
    public class RéférentielAbonnésOnEfCore : RéférentielAbonnés
    {
        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielAbonnésOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }


        public List<Abonné> GetAllAbonnés()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Abonnés.Select(AbonnéMapper.Map).ToList();
            }
        }
    }
}
