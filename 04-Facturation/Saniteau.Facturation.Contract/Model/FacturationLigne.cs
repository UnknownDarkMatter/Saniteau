using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Contract.Model
{
    public class FacturationLigne
    {
        public int IdFacturationLigne { get; set; }
        public int IdFacturation { get; set; }
        public ClasseLigneFacturation ClasseLigneFacturation { get; set; }
        public decimal MontantEuros { get; set; }
        public int ConsommationM3 { get; set; }
        public decimal PrixM3 { get; set; }

        public FacturationLigne() { }
        public FacturationLigne(int idFacturationLigne, int idFacturation, ClasseLigneFacturation classeLigneFacturation, decimal montantEuros, int consommationM3, decimal prixM3)
        {
            IdFacturationLigne = idFacturationLigne;
            IdFacturation = idFacturation;
            ClasseLigneFacturation = classeLigneFacturation;
            MontantEuros = montantEuros;
            ConsommationM3 = consommationM3;
            PrixM3 = prixM3;
        }
    }
}
