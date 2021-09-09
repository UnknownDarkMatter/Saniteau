using Microsoft.EntityFrameworkCore;
using Saniteau.Common;
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
    public class RéférentielFacturationOnEfCore : RéférentielFacturation
    {
        private readonly IDbContextFactory _dbContextFactory;
        private static FacturationModel Insert(FacturationModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Facturations.Add(model);
                dbContext.SaveChanges();
                var facturation = dbContext.Facturations.Include(m=>m.Abonné).Include(m => m.FacturationLignes).FirstOrDefault(m => m.IdFacturation == model.IdFacturation);
                return facturation;
            }
        }

        private static FacturationModel Update(FacturationModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.Facturations.Include(m => m.Abonné).Include(m => m.FacturationLignes).Single(c => c.IdFacturation == model.IdFacturation);
                existingModel.IdAbonné = model.IdAbonné;
                existingModel.DateFacturation = model.DateFacturation;
                existingModel.IdDernierIndex = model.IdDernierIndex;

                //Add
                foreach(var ligne in model.FacturationLignes.Where(m => m.IdFacturationLigne == 0))
                {
                    existingModel.FacturationLignes.Add(ligne);
                }
                //update
                foreach (var ligne in model.FacturationLignes.Where(m => m.IdFacturationLigne > 0))
                {
                    var existingLigne = existingModel.FacturationLignes.FirstOrDefault(m => m.IdFacturationLigne == ligne.IdFacturationLigne);
                    if(existingLigne is null)
                    {
                        existingModel.FacturationLignes.Add(ligne);
                    }
                    else
                    {
                        existingLigne.ClasseLigneFacturation = ligne.ClasseLigneFacturation;
                        existingLigne.ConsommationM3 = ligne.ConsommationM3;
                        existingLigne.MontantEuros = ligne.MontantEuros;
                    }
                }
                //delete
                int i = 0;
                while(i < existingModel.FacturationLignes.Count)
                {
                    var existingLigne = existingModel.FacturationLignes.ToList()[i];
                    if(!model.FacturationLignes.Any(m=>m.IdFacturationLigne == existingLigne.IdFacturationLigne))
                    {
                        existingModel.FacturationLignes.Remove(existingLigne);
                    }
                    else
                    {
                        i++;
                    }
                }

                dbContext.SaveChanges();
                return existingModel;
            }
        }

        public RéférentielFacturationOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public List<Domain.Facturation> GetFacturations(IdAbonné idAbonné)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Facturations.Where(m=>m.IdAbonné == (int) idAbonné).Include(m => m.Abonné).Include(m => m.FacturationLignes).Select(FacturationMapper.Map).ToList();
            }
        }

        public List<Domain.Facturation> GeAllFacturations()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Facturations.Include(m => m.Abonné).Include(m => m.FacturationLignes).Select(FacturationMapper.Map).ToList();
            }
        }

        public Domain.Facturation EnregistrerFacturation(Domain.Facturation facturation)
        {
            var model = FacturationMapper.Map(facturation);
            if ((int)facturation.IdFacturation == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return FacturationMapper.Map(model);
        }

        public int GetDerniereCampagneFacturationId()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var lastFacturation = dbContext.Facturations.OrderByDescending(m => m.IdCampagneFacturation).FirstOrDefault();
                if(lastFacturation != null)
                {
                    return lastFacturation.IdCampagneFacturation;
                }
                return 0;
            }
        }
    }
}
