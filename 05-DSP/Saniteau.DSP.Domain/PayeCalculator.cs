using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class PayeCalculator
    {
        private readonly RéférentielPaye _référentielPaye;
        private readonly RéférentielAbonnés _référentielAbonnés;
        private readonly RéférentielFacturation _référentielFacturation;
        private readonly RéférentielPompes _référentielPompes;
        private readonly RéférentielIndexesCompteurs _référentielIndexesCompteurs;
        private readonly RéférentielCompteurs _référentielCompteurs;

        public PayeCalculator(RéférentielPaye référentielPaye, RéférentielAbonnés référentielAbonnés,
            RéférentielFacturation référentielFacturation, RéférentielPompes référentielPompes, RéférentielIndexesCompteurs référentielIndexesCompteurs,
            RéférentielCompteurs référentielCompteurs)
        {
            _référentielPaye = référentielPaye;
            _référentielAbonnés = référentielAbonnés;
            _référentielFacturation = référentielFacturation;
            _référentielPompes = référentielPompes;
            _référentielIndexesCompteurs = référentielIndexesCompteurs;
            _référentielCompteurs = référentielCompteurs;
        }

        public PayeDelegant CalculePayeDelegant(Date datePaye, IdDelegant idDelegant, out List<IndexPayéParDelegant> nouveauxIndexPayés, out List<FacturePayeeAuDelegant> nouvellesFacturesPayées)
        {
            var nouvellePaye = new PayeDelegant(IdPayeDelegant.Parse(0), idDelegant, datePaye, new List<PayeDelegantLigne>());
            nouveauxIndexPayés = new List<IndexPayéParDelegant>();
            nouvellesFacturesPayées = new List<FacturePayeeAuDelegant>();

            //somme des facturations depuis la paye précédente
            decimal sommeFacturesEuros = SommeFacturesPayees(datePaye, ref nouvellesFacturesPayées);
            AjoutePayeDelegantLigne(nouvellePaye, sommeFacturesEuros, ClasseLignePayeDelegant.FacturationAbonnés);

            //volume de fuites depuis la paye précédente
            var allPompes = _référentielPompes.GetAllPompes();
            var indexesPayes = _référentielPaye.GetIndexesPayesParDelegants();
            var compteursFactures = _référentielCompteurs.GetAllCompteurs().Where(c => !allPompes.Any(p => p.IdCompteur == c.IdCompteur));
            int sommeIndexesCompteurs = SommeDernierIndexesCompteurs(compteursFactures.Select(m => m.IdCompteur).ToList(), datePaye, indexesPayes, ref nouveauxIndexPayés);
            int sommeIndexesPompesM3 = SommeDernierIndexesCompteurs(allPompes.Select(m=>m.IdCompteur).ToList(), datePaye, indexesPayes, ref nouveauxIndexPayés);
            int sommeFuitesM3 = sommeIndexesPompesM3 - sommeIndexesCompteurs > 0 ? sommeIndexesPompesM3 - sommeIndexesCompteurs : 0;
            decimal prixFuitesEuros = (decimal)sommeFuitesM3 * Constantes.PrixM3.Fuites;
            AjoutePayeDelegantLigne(nouvellePaye, prixFuitesEuros, ClasseLignePayeDelegant.ConsommationFuites);

            //retrait rémunération Saniteau
            decimal remunerationSaniteau = (sommeFacturesEuros - prixFuitesEuros) * Constantes.RemunerationSaniteauPourcentage / 100;
            AjoutePayeDelegantLigne(nouvellePaye, remunerationSaniteau, ClasseLignePayeDelegant.RéumnérationSaniteau);

            return nouvellePaye;
        }

        private int SommeDernierIndexesCompteurs(List<IdCompteur> compteurs, Date datePaye, List<IndexPayéParDelegant> indexesPayes, ref List<IndexPayéParDelegant> nouveauxIndexPayés)
        {
            int sommeIndexesCompteurs = 0;
            foreach (var idCompteur in compteurs)
            {
                var dernierIndexPaye = indexesPayes.OrderByDescending(m => (int)m.IdIndexPayéParDelegant).Where(m => m.IdCompteur == idCompteur).FirstOrDefault();
                var indexesCompteur = _référentielIndexesCompteurs.GetIndexesOfCompteur(idCompteur).Where(m => m.DateIndex <= datePaye.ToDateTime(23, 59, 59));
                var indexAPayer = indexesCompteur.OrderByDescending(m => (int)m.IdIndex).Where(m => m.DateIndex <= datePaye.ToDateTime(23, 59, 59)
                                                                    && (dernierIndexPaye == null || (int)dernierIndexPaye.IdIndex < (int)m.IdIndex)).FirstOrDefault();
                if (indexAPayer != null)
                {
                    sommeIndexesCompteurs += indexAPayer.IndexM3;
                    nouveauxIndexPayés.Add(new IndexPayéParDelegant(IdIndexPayéParDelegant.Parse(0), IdPayeDelegant.Parse(0), idCompteur, indexAPayer.IdIndex));
                }
            }
            return sommeIndexesCompteurs;
        }

        private decimal SommeFacturesPayees(Date datePaye, ref List<FacturePayeeAuDelegant> nouvellesFacturesPayées)
        {
            decimal sommeFacturesEuros = 0;
            var facturesPayees = _référentielPaye.GetFacturesPayeesAuDelegants();
            foreach (var abonné in _référentielAbonnés.GetAllAbonnés())
            {
                var derniereFacturationPayee = facturesPayees.Where(m => m.IdAbonné == abonné.IdAbonné).OrderByDescending(m => (int)m.IdFacturePayeeAuDelegant).FirstOrDefault();
                var facturationsOfAbonné = _référentielFacturation.GetFacturations(abonné.IdAbonné);
                var facturationsAPayer = facturationsOfAbonné.Where(m => m.DateFacturation.ToDateTime(0, 0, 0) <= datePaye.ToDateTime(0, 0, 0)
                                                                && (derniereFacturationPayee == null || (int)derniereFacturationPayee.IdFacturation < (int)m.IdFacturation));
                foreach (var facturation in facturationsAPayer)
                {
                    sommeFacturesEuros += facturation.GetMontant();
                    nouvellesFacturesPayées.Add(new FacturePayeeAuDelegant(IdFacturePayeeAuDelegant.Parse(0), IdPayeDelegant.Parse(0), facturation.IdFacturation, abonné.IdAbonné));
                }
            }
            return sommeFacturesEuros;
        }

        private void AjoutePayeDelegantLigne(PayeDelegant payeDelegant,decimal montantEuros, ClasseLignePayeDelegant classe)
        {
            var ligne =  new PayeDelegantLigne(IdPayeDelegantLigne.Parse(0), IdPayeDelegant.Parse(0), classe, Montant.FromDecimal(montantEuros));
            payeDelegant.LignesPaye.Add(ligne);
        }

    }
}
