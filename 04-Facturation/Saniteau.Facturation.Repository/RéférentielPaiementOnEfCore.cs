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
    public class RéférentielPaiementOnEfCore : RéférentielPaiement
    {
        private readonly IDbContextFactory _dbContextFactory;
        private static PaymentModel Insert(PaymentModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Payments.Add(model);
                dbContext.SaveChanges();
                var paiement = dbContext.Payments.Include(m=>m.Facturation).FirstOrDefault(m => m.IdPayment == model.IdPayment);
                return paiement;
            }
        }

        private static PaymentModel Update(PaymentModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.Payments.Include(m => m.Facturation).Single(c => c.IdPayment == model.IdPayment);
                //existingModel.IdFacturation = model.IdFacturation;
                existingModel.PaypalOrderId = model.PaypalOrderId;
                existingModel.PaymentDate = model.PaymentDate;
                existingModel.PaymentStatut = model.PaymentStatut;

                dbContext.SaveChanges();
                return existingModel;
            }
        }

        public RéférentielPaiementOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public List<Paiement> GetPaiements(IdFacturation idFacturation)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Payments.Where(m=>m.IdFacturation == (int)idFacturation).Include(m => m.Facturation).Select(PaiementMapper.Map).ToList();
            }
        }

        public Domain.Paiement EnregistrerPaiement(Domain.Paiement paiement)
        {
            var model = PaiementMapper.Map(paiement);
            if ((int)paiement.IdPaiement == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return PaiementMapper.Map(model);
        }

    }
}
