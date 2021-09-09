using Microsoft.EntityFrameworkCore;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Saniteau.Compteurs.Repository
{
    public class RéférentielAppairageOnEfCore : RéférentielAppairage
    {
        private static AppairageCompteurModel Insert(AppairageCompteurModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.AppairagesCompteurs.Add(model);
                dbContext.SaveChanges();
                return model;
            }
        }

        private static AppairageCompteurModel Update(AppairageCompteurModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.AppairagesCompteurs.Single(c => c.IdPDL == model.IdPDL);
                existingModel.IdCompteur = model.IdCompteur;
                existingModel.DateAppairage = model.DateAppairage;
                existingModel.DateDésappairage = model.DateDésappairage;
                dbContext.SaveChanges();
                return existingModel;
            }
        }
        private static AdressePDLModel Insert(AdressePDLModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.AdressesPDL.Add(model);
                dbContext.SaveChanges();
                return model;
            }
        }

        private static AdressePDLModel Update(AdressePDLModel model, IDbContextFactory dbContextFactory)
        {
            using (var dbContext = dbContextFactory.CreateDbContext())
            {
                var existingModel = dbContext.AdressesPDL.Single(c => c.IdAdressePDL == model.IdAdressePDL);
                existingModel.IdAdresse = model.IdAdresse;
                existingModel.IdPDL = model.IdPDL;
                dbContext.SaveChanges();
                return existingModel;
            }
        }


        private readonly IDbContextFactory _dbContextFactory;

        public RéférentielAppairageOnEfCore(IDbContextFactory dbContextFactory)
        {
            if (dbContextFactory is null) { throw new ArgumentNullException(nameof(dbContextFactory)); }

            _dbContextFactory = dbContextFactory;
        }

        public List<AppairageCompteur> GetAppairageOfPDL(IdPDL idPDL)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.AppairagesCompteurs.Where(c => c.IdPDL == (int)idPDL).Select(AppairageCompteurMapper.Map).ToList();
            }
        }

        public AppairageCompteur GetAppairageOfPDL(IdAppairageCompteur idAppairageCompteur)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var appairage = dbContext.AppairagesCompteurs.FirstOrDefault(c => c.IdAppairageCompteur == (int)idAppairageCompteur);
                return AppairageCompteurMapper.Map(appairage);
            }
        }
        public AdressePDL CréeAdressePDL(AdressePDL adressePDL)
        {
            var model = AdressePDLMapper.Map(adressePDL);
            if ((int)adressePDL.IdAdressePDL == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return AdressePDLMapper.Map(model);

        }

        public List<AdressePDL> GetAdressesPDL(IdAdresse idAdresse)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.AdressesPDL.Where(a => a.IdAdresse == (int)idAdresse).Select(AdressePDLMapper.Map).ToList();
            }
        }

        public List<AdressePDL> GetAdressesPDL(IdPDL idPDL)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.AdressesPDL.Where(a => a.IdPDL == (int)idPDL).Select(AdressePDLMapper.Map).ToList();
            }
        }

        public void SupprimeAdressePDL(IdAdresse idAdresse)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var models = dbContext.AdressesPDL.Where(c => c.IdAdresse == (int)idAdresse);
                foreach (var model in models)
                {
                    dbContext.AdressesPDL.Remove(model);
                }
                dbContext.SaveChanges();
            }
        }

        public AppairageCompteur EnregistreAppairageCompteur(AppairageCompteur appairageCompteur)
        {
            var model = AppairageCompteurMapper.Map(appairageCompteur);
            bool exists = false;
            var appairagesOfPDL = GetAppairageOfPDL(appairageCompteur.IdPDL);
            foreach (var appairageOfPDL in appairagesOfPDL)
            {
                if (appairageOfPDL.IdCompteur == appairageCompteur.IdCompteur)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                model = Insert(model, _dbContextFactory);
            }
            else
            {
                model = Update(model, _dbContextFactory);
            }
            return AppairageCompteurMapper.Map(model);
        }

        public List<AppairageCompteur> GetAppairageOfPDL(IdAdresse idAdresse){
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var query = from adressesPDL in dbContext.AdressesPDL
                            join appairages in dbContext.AppairagesCompteurs on adressesPDL.IdPDL equals appairages.IdPDL
                            where adressesPDL.IdAdresse == (int)idAdresse
                            select appairages;
                return query.Select(AppairageCompteurMapper.Map).ToList();
            }
        }

        public void SupprimeAppairagge(AppairageCompteur appairageCompteur)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var model = dbContext.AppairagesCompteurs.FirstOrDefault(c => c.IdCompteur == (int)appairageCompteur.IdCompteur && c.IdPDL == (int)appairageCompteur.IdPDL);
                if(model == null) { return; }
                dbContext.AppairagesCompteurs.Remove(model);
                dbContext.SaveChanges();
            }
        }
    }
}
