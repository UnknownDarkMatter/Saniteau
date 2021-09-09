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
    public class RéférentielCompteursOnEfCore : RéférentielCompteurs
    {

        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielCompteursOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }


        public Compteur GetCompteur(IdCompteur idCompteur)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var model = dbContext.Compteurs.FirstOrDefault(c => c.IdCompteur == (int)idCompteur);
                return CompteurMapper.Map(model);
            }
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
