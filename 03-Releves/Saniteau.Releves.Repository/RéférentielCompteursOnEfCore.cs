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
    public class RéférentielCompteursOnEfCore : RéférentielCompteurs
    {

        private static CompteurModel Insert(CompteurModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Compteurs.Add(model);
                dbContext.SaveChanges();
                return model;
            }
        }

        private static CompteurModel Update(CompteurModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.Compteurs.Single(c => c.IdCompteur == model.IdCompteur);
                existingModel.NuméroCompteur = model.NuméroCompteur;
                existingModel.CompteurPosé = model.CompteurPosé;
                dbContext.SaveChanges();
                return existingModel;
            }
        }
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
        public Compteur EnregistrerCompteur(Compteur compteur)
        {
            var model = CompteurMapper.Map(compteur);
            if ((int)compteur.IdCompteur == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return CompteurMapper.Map(model);
        }

        public List<Compteur> GetAllCompteurs()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Compteurs.Select(CompteurMapper.Map).ToList();
            }
        }

        public List<Tuple<Compteur, IndexCompteur>> GetAllCompteursIndexes()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var query = from compteur in dbContext.Compteurs
                            join index in dbContext.IndexesCompteur on compteur.IdCompteur equals index.IdCompteur into index2
                            from index in index2.DefaultIfEmpty()
                            where index != null
                            select new
                            {
                                Compteur = compteur,
                                Index = index
                            };

                return query.ToList().Select(m => new Tuple<Compteur, IndexCompteur>(CompteurMapper.Map(m.Compteur), IndexCompteurMapper.Map(m.Index))).ToList();
            }

        }

    }
}
