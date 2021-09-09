using Microsoft.EntityFrameworkCore;
using Saniteau.Common;
using Saniteau.DSP.Domain;
using Saniteau.DSP.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Repository
{
    public class RéférentielFacturationOnEfCore : RéférentielFacturation
    {
        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielFacturationOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public List<Domain.Facturation> GetFacturations(IdAbonné idAbonné)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Facturations.Include(m => m.Abonné).Include(m => m.FacturationLignes).Select(FacturationMapper.Map).ToList();
            }
        }
    }
}
