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
    public class RéférentielAbonnésOnEfCore : RéférentielAbonnés
    {
        private static AdresseModel Insert(AdresseModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Adresses.Add(model);
                dbContext.SaveChanges();
                return model;
            }
        }

        private static AdresseModel Update(AdresseModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.Adresses.Single(c => c.IdAdresse == model.IdAdresse);
                existingModel.NuméroEtRue = model.NuméroEtRue;
                existingModel.Ville = model.Ville;
                existingModel.CodePostal = model.CodePostal;
                dbContext.SaveChanges();
                return existingModel;
            }
        }

        private static AbonnéModel Insert(AbonnéModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Abonnés.Add(model);
                dbContext.SaveChanges();
                return model;
            }
        }

        private static AbonnéModel Update(AbonnéModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.Abonnés.Single(c => c.IdAbonné == model.IdAbonné);
                existingModel.IdAdresse = model.IdAdresse;
                existingModel.Nom = model.Nom;
                existingModel.Prénom = model.Prénom;
                dbContext.SaveChanges();
                return existingModel;
            }
        }

        private readonly IDbContextFactory _dbContextFactory;

        public RéférentielAbonnésOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public Abonné EnregistreAbonné(Abonné abonné)
        {
            var adresse = GetAddresse(abonné.IdAdresse);
            if (adresse is null) { throw new BusinessException($"L'adresse de l'abonné doit être crée avant de créer l'abonné (IdAbonné={abonné.IdAbonné}, IDAdresse={abonné.IdAdresse})."); }

            var model = AbonnéMapper.Map(abonné);
            if ((int)abonné.IdAbonné == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return AbonnéMapper.Map(model);
        }

        public Adresse CréeAdresse(Adresse adresse)
        {
            var model = AdresseMapper.Map(adresse);
            if ((int)adresse.IdAdresse == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return AdresseMapper.Map(model);
        }

        public Adresse GetAddresseOfAbonné(IdAbonné idAbonné)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var abonnéModel = dbContext.Abonnés.FirstOrDefault(a => a.IdAbonné == (int)idAbonné);
                if(abonnéModel == null) { return null; }
                var adresseModel = dbContext.Adresses.FirstOrDefault(a => a.IdAdresse == abonnéModel.IdAdresse);
                if (adresseModel == null) { return null; }

                return AdresseMapper.Map(adresseModel);
            }
        }

        public Adresse GetAddresse(IdAdresse idAdresse)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var adresseModel = dbContext.Adresses.FirstOrDefault(a => a.IdAdresse == (int)idAdresse);
                if (adresseModel == null) { return null; }

                return AdresseMapper.Map(adresseModel);
            }
        }

        public List<Adresse> GetAllAddresses()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Adresses.Select(AdresseMapper.Map).ToList();
            }
        }

        public Abonné GetAbonné(IdAbonné idAbonné)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var model = dbContext.Abonnés.FirstOrDefault(a => a.IdAbonné == (int)idAbonné);
                if (model == null) { return null; }

                return AbonnéMapper.Map(model);
            }
        }

        public List<Abonné> GetAllAbonnés()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.Abonnés.Select(AbonnéMapper.Map).ToList();
            }
        }
    }
}
