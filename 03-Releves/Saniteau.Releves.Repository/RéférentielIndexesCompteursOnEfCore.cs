using Saniteau.Releves.Domain;
using Saniteau.Releves.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Releves.Repository
{
    public class RéférentielIndexesCompteursOnEfCore : RéférentielIndexesCompteurs
    {
        private static IndexCompteurModel Insert(IndexCompteurModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.IndexesCompteur.Add(model);
                dbContext.SaveChanges();
                return model;
            }
        }

        private static IndexCompteurModel Update(IndexCompteurModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.IndexesCompteur.Single(c => c.IdIndex == model.IdIndex);
                existingModel.IndexM3 = model.IndexM3;
                existingModel.IdCompteur = model.IdCompteur;
                existingModel.DateIndex = model.DateIndex;
                dbContext.SaveChanges();
                return existingModel;
            }
        }

        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielIndexesCompteursOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }


        public IndexCompteur EnregistreIndex(IndexCompteur index)
        {
            IndexCompteurModel model = IndexCompteurMapper.Map(index);
            if ((int)index.IdIndex == 0)
            {
                model = Insert(model, _dbContextFactory);
            } else
            {
                model = Update(model, _dbContextFactory);
            }
            return IndexCompteurMapper.Map(model);
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
