using Saniteau.Common;
using Saniteau.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Database
{
    public class SaniteauDbContextFactory : IDbContextFactory
    {
        private static object _lockObject = new object();
        public SaniteauDbContextFactory()
        {
        }

        public SaniteauDbContext CreateDbContext()
        {
            var dbContext = new SaniteauDbContext();
            lock(_lockObject)
            {
                if (!ExistePompePrincipale(dbContext))
                {
                    CréePompePrincipale(dbContext);
                }
            }
            return dbContext;
        }

        private bool ExistePompePrincipale(SaniteauDbContext dbContext)
        {
            return dbContext.Compteurs.Any(m=>m.NuméroCompteur == Constants.Database.NomCompteurPompePrincipale);
        }

        private void CréePompePrincipale(SaniteauDbContext dbContext)
        {
            var compteur = new Infrastructure.DataModel.CompteurModel(0, Constants.Database.NomCompteurPompePrincipale, true);
            dbContext.Compteurs.Add(compteur);
            dbContext.SaveChanges();
            var pompe = new Infrastructure.DataModel.PompeModel(0, compteur.IdCompteur, Constants.Database.NomPompePrincipale);
            dbContext.Pompes.Add(pompe);
            var index = new Infrastructure.DataModel.IndexCompteurModel(0, pompe.IdCompteur, 0, 0, Horloge.Instance.GetDateTime());
            dbContext.IndexesCompteur.Add(index);
            dbContext.SaveChanges();
        }
    }
}
