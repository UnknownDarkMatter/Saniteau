using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Repository
{
    public class RéférentielPDLOnEfCore : RéférentielPDL
    {
        private static PDLModel Insert(PDLModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.PDL.Add(model);
                dbContext.SaveChanges();
                return model;
            }
        }

        private static PDLModel Update(PDLModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.PDL.Single(p => p.IdPDL == model.IdPDL);
                existingModel.NuméroPDL = model.NuméroPDL;
                dbContext.SaveChanges();
                return existingModel;
            }
        }

        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielPDLOnEfCore(IDbContextFactory dbContextFactory)
        {
            if(dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public PDL CréeNouveauPDL(PDL pdl)
        {
            var model = PDLMapper.Map(pdl);
            if ((int)model.IdPDL == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return PDLMapper.Map(model);
        }

        public PDL GetPDL(IdPDL idPDL)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var model = dbContext.PDL.Where(a => a.IdPDL == (int)idPDL).FirstOrDefault();
                return PDLMapper.Map(model);
            }
        }
        public void SupprimePDL(IdPDL idPDL)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var model = dbContext.PDL.Where(a => a.IdPDL == (int)idPDL).FirstOrDefault();
                dbContext.PDL.Remove(model);
                dbContext.SaveChanges();
            }
        }
    }
}
