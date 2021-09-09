using Saniteau.Models.Abonnés;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Models.Facturation
{
    public class Facturation
    {
        public int IdFacturation { get; set; }
        public int IdCampagneFacturation { get; set; }
        public Abonne Abonne { get; set; }
        public DateTime DateFacturation { get; set; }
        public List<FacturationLigne> FacturationLignes { get; set; }
        public int IdDernierIndex { get; set; }
        public bool Payee { get; set; }

        public Facturation() { }
        public Facturation(int idFacturation, int idCampagneFacturation, Abonne abonne, DateTime dateFacturation, List<FacturationLigne> facturationLignes, int idDernierIndex, bool payee) {
            IdFacturation = idFacturation;
            IdCampagneFacturation = idCampagneFacturation;
            Abonne = abonne;
            DateFacturation = dateFacturation;
            FacturationLignes = facturationLignes;
            IdDernierIndex = idDernierIndex;
            Payee = payee;
        }
    }
}
