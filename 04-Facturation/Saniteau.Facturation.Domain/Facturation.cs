using Saniteau.Common;
using Saniteau.Facturation.Domain.Calcul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class Facturation
    {
        public IdFacturation IdFacturation { get; private set; }
        public int IdCampagneFacturation { get; set; }
        public Abonné Abonné { get; private set; }
        public DateTime DateFacturation { get; private set; }
        public List<FacturationLigne> LignesFacturation { get; private set; }
        public IdIndex IdDernierIndex { get; private set; }
        public bool Payée { get; private set; }

        private Facturation(int idCampagneFacturation, Abonné abonné, DateTime dateFacturation, IdIndex idDernierIndex, bool payée)
        {
            if (abonné is null) { throw new ArgumentNullException(nameof(abonné)); }

            IdFacturation = IdFacturation.Parse(0);
            IdCampagneFacturation = idCampagneFacturation;
            Abonné = abonné;
            DateFacturation = dateFacturation;
            IdDernierIndex = idDernierIndex;
            LignesFacturation = new List<FacturationLigne>();
            Payée = payée;
        }

        public Facturation(IdFacturation idFacturation, int idCampagneFacturation, Abonné abonné, DateTime dateFacturation, List<FacturationLigne> lignesFacturation, IdIndex idDernierIndex, bool payée)
        {
            if (idFacturation is null) { throw new ArgumentNullException(nameof(idFacturation)); }
            if (abonné is null) { throw new ArgumentNullException(nameof(abonné)); }
            if (lignesFacturation is null) { throw new ArgumentNullException(nameof(lignesFacturation)); }

            IdFacturation = idFacturation;
            IdCampagneFacturation = idCampagneFacturation;
            Abonné = abonné;
            DateFacturation = dateFacturation;
            IdDernierIndex = idDernierIndex;
            LignesFacturation = lignesFacturation;
            Payée = payée;
        }

        public static Facturation CalculeFacturation(int idCampagneFacturation, DateTime dateFacturation, IdAbonné idAbonné, 
            RéférentielAbonnés référentielAbonnés , RéférentielAppairage référentielAppairage,
            RéférentielIndexesCompteurs référentielIndexesCompteurs, RéférentielFacturation référentielFacturation)
        {
            Abonné abonné = référentielAbonnés.GetAbonné(idAbonné);
            List<AdressePDL> addressPDLs = new List<AdressePDL>();
            if(abonné != null)
            {
                addressPDLs = référentielAppairage.GetAdressesPDL(abonné.IdAdresse);
            }
            IdCompteur idCompteur = null;
            foreach(var adressePDL in addressPDLs)
            {
                var appairages = référentielAppairage.GetAppairageOfPDL(adressePDL.IdPDL);
                foreach(var appairage in appairages)
                {
                    if(appairage.DateAppairage != null && appairage.DateDésappairage == null && appairage.DateAppairage.ToDateTime(0,0,0) <= dateFacturation)
                    {
                        idCompteur = appairage.IdCompteur;
                        break;
                    }
                }
                if(idCompteur != null)
                {
                    break;
                }
            }
            List<IndexCompteur> indexesCompteurs = new List<IndexCompteur>();
            if(idCompteur != null)
            {
                indexesCompteurs = référentielIndexesCompteurs.GetIndexesOfCompteur(idCompteur);
            }
            List<Facturation> facturationsPrécédentes = référentielFacturation.GetFacturations(idAbonné);
            return CalculeFacturation(idCampagneFacturation, dateFacturation, abonné, indexesCompteurs, facturationsPrécédentes);
        }

        private static Facturation CalculeFacturation(int idCampagneFacturation, DateTime dateFacturation, Abonné abonné, List<IndexCompteur> indexesCompteurs, List<Facturation> facturationsPrecedentes)
        {
            if (abonné is null) { throw new ArgumentNullException(nameof(abonné)); }
            if (indexesCompteurs is null) { throw new ArgumentNullException(nameof(indexesCompteurs)); }
            if (facturationsPrecedentes is null) { throw new ArgumentNullException(nameof(facturationsPrecedentes)); }

            var sortedIndexes = indexesCompteurs.OrderByDescending(m => (int)m.IdIndex).ToList();
            IndexCompteur dernierIndex = sortedIndexes.FirstOrDefault();

            Facturation facturation = new Facturation(idCampagneFacturation, abonné, dateFacturation, dernierIndex?.IdIndex, false);
            AjouteAbonnementRule ajouteAbonnementRule = new AjouteAbonnementRule(facturation, facturationsPrecedentes);
            if (ajouteAbonnementRule.IsSatisfied())
            {
                ajouteAbonnementRule.ExecuteRule();
            }
            AjouteConsoRéelleRule consoRéelleRule = new AjouteConsoRéelleRule(facturation, facturationsPrecedentes, indexesCompteurs);
            if (consoRéelleRule.IsSatisfied())
            {
                consoRéelleRule.ExecuteRule();
            }
            return facturation;
        }



    }
}
