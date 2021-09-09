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
    public class RéférentielDelegantOnEfCore : RéférentielDelegant
    {
        private static DelegantModel Insert(DelegantModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Delegants.Add(model);
                dbContext.SaveChanges();
                return dbContext.Delegants.FirstOrDefault(m => m.IdDelegant == model.IdDelegant);
            }
        }

        private static DelegantModel Update(DelegantModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.Delegants.FirstOrDefault(m => m.IdDelegant == model.IdDelegant);
                existingModel.Nom = model.Nom;
                existingModel.Adresse = model.Adresse;
                existingModel.DateContrat = model.DateContrat;
                dbContext.SaveChanges();
                return existingModel;
            }
        }

        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielDelegantOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public Delegant EnregistreDelegant(Delegant delegant)
        {
            var model = DelegantMapper.Map(delegant);
            if ((int)delegant.IdDelegant == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return DelegantMapper.Map(model);
        }

        public List<Delegant> GetDelegants()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Delegants.Select(DelegantMapper.Map).ToList();
            }
        }
    }
}
