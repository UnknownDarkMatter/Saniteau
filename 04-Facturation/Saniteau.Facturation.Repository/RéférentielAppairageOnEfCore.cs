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
    public class RéférentielAppairageOnEfCore : RéférentielAppairage
    {

        private readonly IDbContextFactory _dbContextFactory;

        public RéférentielAppairageOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public List<AppairageCompteur> GetAppairageOfPDL(IdPDL idPDL)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.AppairagesCompteurs.Where(c => c.IdPDL == (int)idPDL).Select(AppairageCompteurMapper.Map).ToList();
            }
        }

        public List<AdressePDL> GetAdressesPDL(IdAdresse idAdresse)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.AdressesPDL.Where(a => a.IdAdresse == (int)idAdresse).Select(AdressePDLMapper.Map).ToList();
            }
        }

    }
}
