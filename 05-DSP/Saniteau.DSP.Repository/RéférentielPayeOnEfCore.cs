using Microsoft.EntityFrameworkCore;
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
    public class RéférentielPayeOnEfCore : RéférentielPaye
    {
        private static PayeDelegantModel Insert(PayeDelegantModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.PayesDelegant.Add(model);
                dbContext.SaveChanges();
                return dbContext.PayesDelegant.Include(m => m.LignesPaye).FirstOrDefault(m => m.IdPayeDelegant == model.IdPayeDelegant);
            }
        }
        private static PayeDelegantModel Update(PayeDelegantModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.PayesDelegant.Include(m => m.LignesPaye).FirstOrDefault(m => m.IdPayeDelegant == model.IdPayeDelegant);
                existingModel.IdDelegant = model.IdDelegant;
                existingModel.DatePaye = model.DatePaye;

                //Add
                foreach (var ligne in model.LignesPaye.Where(m => m.IdPayeDelegantLigne == 0))
                {
                    existingModel.LignesPaye.Add(ligne);
                }
                //update
                foreach (var ligne in model.LignesPaye.Where(m => m.IdPayeDelegantLigne > 0))
                {
                    var existingLigne = existingModel.LignesPaye.FirstOrDefault(m => m.IdPayeDelegantLigne == ligne.IdPayeDelegantLigne);
                    if (existingLigne is null)
                    {
                        existingModel.LignesPaye.Add(ligne);
                    }
                    else
                    {
                        existingLigne.Classe = ligne.Classe;
                        existingLigne.MontantEuros = ligne.MontantEuros;
                        existingLigne.IdPayeDelegant = ligne.IdPayeDelegant;
                    }
                }
                //delete
                int i = 0;
                while (i < existingModel.LignesPaye.Count)
                {
                    var existingLigne = existingModel.LignesPaye.ToList()[i];
                    if (!model.LignesPaye.Any(m => m.IdPayeDelegantLigne == existingLigne.IdPayeDelegantLigne))
                    {
                        existingModel.LignesPaye.Remove(existingLigne);
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
        private static FacturePayeeAuDelegantModel Insert(FacturePayeeAuDelegantModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.FacturesPayeesAuDelegant.Add(model);
                dbContext.SaveChanges();
                return dbContext.FacturesPayeesAuDelegant.FirstOrDefault(m => m.IdFacturePayeeAuDelegant == model.IdFacturePayeeAuDelegant);
            }
        }
        private static FacturePayeeAuDelegantModel Update(FacturePayeeAuDelegantModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.FacturesPayeesAuDelegant.FirstOrDefault(m => m.IdFacturePayeeAuDelegant == model.IdFacturePayeeAuDelegant);
                existingModel.IdAbonné = model.IdAbonné;
                existingModel.IdFacturation = model.IdFacturation;
                existingModel.IdPayeDelegant = model.IdPayeDelegant;
                dbContext.SaveChanges();
                return existingModel;
            }
        }
        private static IndexPayéParDelegantModel Insert(IndexPayéParDelegantModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.IndexesPayésParDelegant.Add(model);
                dbContext.SaveChanges();
                return dbContext.IndexesPayésParDelegant.FirstOrDefault(m => m.IdIndexPayéParDelegant == model.IdIndexPayéParDelegant);
            }
        }
        private static IndexPayéParDelegantModel Update(IndexPayéParDelegantModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.IndexesPayésParDelegant.FirstOrDefault(m => m.IdIndexPayéParDelegant == model.IdIndexPayéParDelegant);
                existingModel.IdCompteur = model.IdCompteur;
                existingModel.IdIndex = model.IdIndex;
                existingModel.IdPayeDelegant = model.IdPayeDelegant;
                dbContext.SaveChanges();
                return existingModel;
            }
        }


        private readonly IDbContextFactory _dbContextFactory;
        public RéférentielPayeOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public PayeDelegant EnregistrePayeDelegant(PayeDelegant payeDelegant)
        {
            var model = PayeDelegantMapper.Map(payeDelegant);
            if ((int)payeDelegant.IdPayeDelegant == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return PayeDelegantMapper.Map(model);
        }

        public List<PayeDelegant> GetPayesDelegants()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.PayesDelegant.Include(m => m.LignesPaye).Select(PayeDelegantMapper.Map).ToList();
            }
        }

        public List<FacturePayeeAuDelegant> GetFacturesPayeesAuDelegants()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.FacturesPayeesAuDelegant.Select(FacturePayeeAuDelegantMapper.Map).ToList();
            }
        }

        public FacturePayeeAuDelegant EnregistreFacturePayeeAuDelegant(FacturePayeeAuDelegant facturePayeeAuDelegant)
        {
            var model = FacturePayeeAuDelegantMapper.Map(facturePayeeAuDelegant);
            if ((int)facturePayeeAuDelegant.IdFacturePayeeAuDelegant == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return FacturePayeeAuDelegantMapper.Map(model);
        }

        public List<IndexPayéParDelegant> GetIndexesPayesParDelegants()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.IndexesPayésParDelegant.Select(IndexPayéParDelegantMapper.Map).ToList();
            }
        }

        public IndexPayéParDelegant EnregistreIndexesPayésParDelegant(IndexPayéParDelegant indexesPayésParDelegant)
        {
            var model = IndexPayéParDelegantMapper.Map(indexesPayésParDelegant);
            if ((int)indexesPayésParDelegant.IdIndexPayéParDelegant == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return IndexPayéParDelegantMapper.Map(model);
        }
    }
}
