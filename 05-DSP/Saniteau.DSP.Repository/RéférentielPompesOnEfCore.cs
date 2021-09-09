using Saniteau.DSP.Domain;
using Saniteau.DSP.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Repository
{
    public class RéférentielPompesOnEfCore : RéférentielPompes
    {
        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielPompesOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public List<Pompe> GetAllPompes()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Pompes.Select(PompeMapper.Map).ToList();
            }
        }
    }
}
