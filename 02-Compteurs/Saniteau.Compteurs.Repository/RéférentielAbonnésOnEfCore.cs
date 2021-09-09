using Saniteau.Common;
using Saniteau.Compteurs.Domain;
using Saniteau.Compteurs.Repository.Mappers;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Compteurs.Repository
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
                existingModel.Tarification = model.Tarification;
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

        public Adresse EnregistreAdresse(Adresse adresse)
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
                if (abonnéModel == null) { return null; }
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

        public Abonné GetAbonné(IdAbonné idAbonné)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var model = dbContext.Abonnés.FirstOrDefault(a => a.IdAbonné == (int)idAbonné);
                if (model == null) { return null; }

                return AbonnéMapper.Map(model);
            }
        }

        public List<Tuple<Abonné, Adresse>> GetAllAbonnés(bool filtrerAbonnésAvecCompteur)
        {

            using (var dbContext = _dbContextFactory.CreateDbContext())
            {

                if (!filtrerAbonnésAvecCompteur)
                {
                    var queryAbonnes = from abonne in dbContext.Abonnés
                                       join adresse in dbContext.Adresses on abonne.IdAdresse equals adresse.IdAdresse
                                       select new Tuple<Abonné, Adresse>(AbonnéMapper.Map(abonne), AdresseMapper.Map(adresse));

                    return queryAbonnes.ToList();
                }

                var queryCompteurs = from adressePDL in dbContext.AdressesPDL
                                     join appairages in dbContext.AppairagesCompteurs on adressePDL.IdPDL equals appairages.IdPDL
                                     where appairages.IdCompteur != null
                                     select adressePDL;

                var queryAbonnesSansCompteurs = from abonne in dbContext.Abonnés
                                                join adresse in dbContext.Adresses on abonne.IdAdresse equals adresse.IdAdresse
                                                join compteurs in queryCompteurs on adresse.IdAdresse equals compteurs.IdAdresse  into abonneCompteurs
                                                from abonneCompteur in abonneCompteurs.DefaultIfEmpty()
                                                where abonneCompteur == null
                                                select new Tuple<Abonné, Adresse>(AbonnéMapper.Map(abonne), AdresseMapper.Map(adresse));


                var result = queryAbonnesSansCompteurs.ToList();
                return result;
                /*
SELECT [a].[IdAbonné], [a].[IdAdresse], [a].[Nom], [a].[Prénom], [a].[Tarification], [a0].[IdAdresse], [a0].[CodePostal], [a0].[NuméroEtRue], [a0].[Ville]
FROM [Abonnés] AS [a]
INNER JOIN [Adresses] AS [a0] ON [a].[IdAdresse] = [a0].[IdAdresse]
LEFT JOIN (
    SELECT [a1].[IdAdresse]
    FROM [AdressesPDL] AS [a1]
    INNER JOIN [AppairagesCompteurs] AS [a2] ON [a1].[IdPDL] = [a2].[IdPDL]
    WHERE [a2].[IdCompteur] IS NOT NULL
) AS [t] ON [a0].[IdAdresse] = [t].[IdAdresse]
WHERE [t].[IdAdresse] IS NULL
*/
                /*
SELECT <select_list> 
FROM Table_A A
LEFT JOIN Table_B B
ON A.Key = B.Key
WHERE B.Key IS NULL

var result = from a in Table_A
join b in Table_B on a.Key equals b.Key into j
from b in j.DefaultIfEmpty()
where b == null
select new { ... };

 */

            }
        }

        public void SupprimeAbonné(IdAbonné idAbonné)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var abonné = dbContext.Abonnés.First(m => m.IdAbonné == (int)idAbonné);
                dbContext.Abonnés.Remove(abonné);
                dbContext.SaveChanges();
            }
        }
        public void SupprimeAdresse(IdAdresse idAdresse)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var adresse = dbContext.Adresses.First(m => m.IdAdresse == (int)idAdresse);
                dbContext.Adresses.Remove(adresse);
                dbContext.SaveChanges();
            }
        }

    }
}
