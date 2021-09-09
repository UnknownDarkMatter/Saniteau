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
    public class RéférentielPompesOnEfCore : RéférentielPompes
    {

        private static PompeModel Insert(PompeModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Pompes.Add(model);
                dbContext.SaveChanges();
                return model;
            }
        }

        private static PompeModel Update(PompeModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.Pompes.Single(c => c.IdCompteur == model.IdCompteur);
                existingModel.NuméroPompe = model.NuméroPompe;
                existingModel.IdCompteur = model.IdCompteur;
                dbContext.SaveChanges();
                return existingModel;
            }
        }
        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielPompesOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public Pompe EnregistrerPompe(Pompe pompe)
        {
            var model = PompeMapper.Map(pompe);
            if ((int)pompe.IdPompe == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return PompeMapper.Map(model);
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
