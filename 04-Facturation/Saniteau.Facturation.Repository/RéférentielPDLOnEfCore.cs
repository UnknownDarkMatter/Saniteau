using Saniteau.Facturation.Domain;
using Saniteau.Facturation.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Repository
{
    public class RéférentielPDLOnEfCore : RéférentielPDL
    {

        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielPDLOnEfCore(IDbContextFactory dbContextFactory)
        {
            if(dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public PDL GetPDL(IdPDL idPDL)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var model = dbContext.PDL.Where(a => a.IdPDL == (int)idPDL).FirstOrDefault();
                return PDLMapper.Map(model);
            }
        }
    }
}
