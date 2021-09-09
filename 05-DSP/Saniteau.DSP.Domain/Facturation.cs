using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class Facturation
    {
        public IdFacturation IdFacturation { get; private set; }
        public int IdCampagneFacturation { get; set; }
        public Abonné Abonné { get; private set; }
        public Date DateFacturation { get; private set; }
        public List<FacturationLigne> LignesFacturation { get; private set; }
        public IdIndex IdDernierIndex { get; private set; }
        public bool Payée { get; private set; }

        public Facturation(IdFacturation idFacturation, int idCampagneFacturation, Abonné abonné, Date dateFacturation, List<FacturationLigne> lignesFacturation, IdIndex idDernierIndex, bool payée)
        {
            if (idFacturation is null) { throw new ArgumentNullException(nameof(idFacturation)); }
            if (abonné is null) { throw new ArgumentNullException(nameof(abonné)); }
            if (dateFacturation is null) { throw new ArgumentNullException(nameof(dateFacturation)); }
            if (lignesFacturation is null) { throw new ArgumentNullException(nameof(lignesFacturation)); }

            IdFacturation = idFacturation;
            IdCampagneFacturation = idCampagneFacturation;
            Abonné = abonné;
            DateFacturation = dateFacturation;
            IdDernierIndex = idDernierIndex;
            LignesFacturation = lignesFacturation;
            Payée = payée;
        }

        public decimal GetMontant()
        {
            decimal montant = 0;
            foreach(var ligne in LignesFacturation)
            {
                montant += (decimal)ligne.MontantEuros;
            }
            return montant;
        }
    }
}
