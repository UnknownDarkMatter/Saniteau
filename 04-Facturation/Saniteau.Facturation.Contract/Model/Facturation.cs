using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Contract.Model
{
    public class Facturation
    {
        public int IdFacturation { get; set; }
        public int IdCampagneFacturation { get; set; }
        public Abonné Abonné { get; set; }
        public DateTime DateFacturation { get; set; }
        public List<FacturationLigne> FacturationLignes { get; set; }
        public int IdDernierIndex { get; set; }
        public bool Payée { get; set; }

        public Facturation() { }
        public Facturation(int idFacturation, int idCampagneFacturation, Abonné abonné, DateTime dateFacturation, List<FacturationLigne> facturationLignes, int idDernierIndex, bool payée) {
            IdFacturation = idFacturation;
            IdCampagneFacturation = idCampagneFacturation;
            Abonné = abonné;
            DateFacturation = dateFacturation;
            FacturationLignes = facturationLignes;
            IdDernierIndex = idDernierIndex;
            Payée = payée;
        }
    }
}
