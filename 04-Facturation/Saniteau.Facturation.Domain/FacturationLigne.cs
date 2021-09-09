using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class FacturationLigne
    {
        public IdFacturationLigne IdFacturationLigne { get; private set; }
        public IdFacturation IdFacturation { get; private set; }
        public ClasseLigneFacturation Classe { get; private set; }
        public Montant MontantEuros { get; private set; }
        public int ConsommationM3 { get; private set; }
        public Montant PrixM3 { get; set; }
        public FacturationLigne(IdFacturationLigne idFacturationLigne, IdFacturation idFacturation, ClasseLigneFacturation classe, Montant montantEuros, int consommationM3, Montant prixM3)
        {
            if (idFacturationLigne is null) { throw new ArgumentNullException(nameof(idFacturationLigne)); }
            if (idFacturation is null) { throw new ArgumentNullException(nameof(idFacturation)); }
            if (montantEuros is null) { throw new ArgumentNullException(nameof(montantEuros)); }
            if (classe == ClasseLigneFacturation.NonDéfini) { throw new ArgumentOutOfRangeException(nameof(classe)); }

            IdFacturationLigne = idFacturationLigne;
            IdFacturation = idFacturation;
            Classe = classe;
            MontantEuros = montantEuros;
            ConsommationM3 = consommationM3;
            PrixM3 = prixM3;
        }

        public void SetClasse(ClasseLigneFacturation classe)
        {
            Classe = classe;
        }

        public void SetMontantEuros(Montant montant)
        {
            MontantEuros = montant;
        }

        public void SetConsommationM3(int consommationM3)
        {
            ConsommationM3 = consommationM3;
        }

    }
}
