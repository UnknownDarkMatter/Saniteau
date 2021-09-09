using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Infrastructure.DataModel;
using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Repository
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

        public Compteur CréerCompteur(ChampLibre numéroCompteur)
        {
            var model = new CompteurModel(0, numéroCompteur.ToString(), false);
            model = Insert(model, _dbContextFactory);
            return CompteurMapper.Map(model, false, null, 0);
        }

        public Compteur EnregistrerCompteur(Compteur compteur)
        {
            var model = CompteurMapper.Map(compteur);
            if ((int)compteur.IdCompteur == 0)
            {
                model = Insert(model, _dbContextFactory);
            }
            else {
                model = Update(model, _dbContextFactory);
            }
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var query = from compteurNotNullable in dbContext.Compteurs
                            from nullableAppairage in dbContext.AppairagesCompteurs.DefaultIfEmpty()
                            join addressePdl in dbContext.AdressesPDL on nullableAppairage.IdPDL equals addressePdl.IdPDL into allAddressePdl
                            from nullableAddressePdl in allAddressePdl.DefaultIfEmpty()
                            join pdl in dbContext.PDL on nullableAddressePdl.IdPDL equals pdl.IdPDL into allPDL
                            from nullablePDL in allPDL.DefaultIfEmpty()
                            where compteurNotNullable.IdCompteur == (int)compteur.IdCompteur && (nullableAppairage == null || compteurNotNullable.IdCompteur == nullableAppairage.IdCompteur)
                            select new { Compteur = compteurNotNullable, CompteurAppairé = EstApairé(nullableAppairage), PDL = nullablePDL };

                var result = query.Select(m => CompteurMapper.Map(m.Compteur, m.CompteurAppairé, m.PDL, 0)).FirstOrDefault();
                return result;
            }

        }

        public Compteur GetCompteur(IdCompteur idCompteur)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var query = from compteur in dbContext.Compteurs
                            join appairage in dbContext.AppairagesCompteurs on compteur.IdCompteur equals appairage.IdCompteur into appairage2
                            from appairage in appairage2.DefaultIfEmpty()
                            join addressePdl in dbContext.AdressesPDL on appairage.IdPDL equals addressePdl.IdPDL into addressePdl2
                            from addressePdl in addressePdl2.DefaultIfEmpty()
                            join pdl in dbContext.PDL on addressePdl.IdPDL equals pdl.IdPDL into pdl2
                            from pdl in pdl2.DefaultIfEmpty()
                            join abonne in dbContext.Abonnés on addressePdl.IdAdresse equals abonne.IdAdresse into abonne2
                            from abonne in abonne2.DefaultIfEmpty()
                            where compteur.IdCompteur == (int)idCompteur && (appairage == null || compteur.IdCompteur == appairage.IdCompteur)
                            select new { Compteur = compteur, CompteurAppairé = EstApairé(appairage), PDL = pdl == null ? default(PDLModel) : pdl, IdAbonné = abonne != null ? abonne.IdAbonné : 0 };

                var result = query.Select(m => CompteurMapper.Map(m.Compteur, m.CompteurAppairé, m.PDL, m.IdAbonné)).FirstOrDefault();
                return result;
            }
        }

        private static bool EstApairé(AppairageCompteurModel appairage)
        {
            if(appairage ==null) { return false; }
            var appairageOFDomain = new AppairageCompteur(IdAppairageCompteur.Parse(appairage.IdAppairageCompteur), IdPDL.Parse(0), IdCompteur.Parse(0), appairage.DateAppairage.ToDate(), appairage.DateDésappairage.ToDate());
            return appairageOFDomain.EstAppairé();
        }

        public IEnumerable<Compteur> GetAllCompteurs()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var query = from compteur in dbContext.Compteurs
                            join appairage in dbContext.AppairagesCompteurs on compteur.IdCompteur equals appairage.IdCompteur into appairage2
                            from appairage in appairage2.DefaultIfEmpty()
                            join addressePdl in dbContext.AdressesPDL on appairage.IdPDL equals addressePdl.IdPDL into addressePdl2
                            from addressePdl in addressePdl2.DefaultIfEmpty()
                            join pdl in dbContext.PDL on addressePdl.IdPDL equals pdl.IdPDL into pdl2
                            from pdl in pdl2.DefaultIfEmpty()
                            join abonne in dbContext.Abonnés on addressePdl.IdAdresse equals abonne.IdAdresse into abonne2
                            from abonne in abonne2.DefaultIfEmpty()
                            select new { Compteur = compteur, CompteurAppairé = EstApairé(appairage), PDL = pdl == null ? default(PDLModel) : pdl, IdAbonné = abonne != null ? abonne.IdAbonné : 0 };

                var result = query.Select(m => CompteurMapper.Map(m.Compteur, m.CompteurAppairé, m.PDL, m.IdAbonné)).ToList();
                return result;
            }
        }

        public void SupprimerCompteur(IdCompteur idCompteur)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var model = dbContext.Compteurs.FirstOrDefault(c => c.IdCompteur == (int)idCompteur);
                if (model is null) { return; }

                dbContext.Compteurs.Remove(model);
                dbContext.SaveChanges();
            }
        }
    }
}
