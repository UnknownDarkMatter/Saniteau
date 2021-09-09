using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class PayeDelegantLigne
    {
        public IdPayeDelegantLigne IdPayeDelegantLigne { get; private set; }
        public IdPayeDelegant IdPayeDelegant { get; private set; }
        public ClasseLignePayeDelegant Classe { get; private set; }
        public Montant MontantEuros { get; private set; }

        public PayeDelegantLigne(IdPayeDelegantLigne idPayeDelegantLigne, IdPayeDelegant idPayeDelegant, ClasseLignePayeDelegant classe, Montant montantEuros)
        {
            if (idPayeDelegantLigne is null) { throw new ArgumentNullException(nameof(idPayeDelegantLigne)); }
            if (idPayeDelegant is null) { throw new ArgumentNullException(nameof(idPayeDelegant)); }
            if (montantEuros is null) { throw new ArgumentNullException(nameof(montantEuros)); }
            if (classe == ClasseLignePayeDelegant.NonDéfini) { throw new ArgumentOutOfRangeException(nameof(classe)); }

            IdPayeDelegantLigne = idPayeDelegantLigne;
            IdPayeDelegant = idPayeDelegant;
            Classe = classe;
            MontantEuros = montantEuros;
        }

        public void ChangeMontantEuros(Montant montantEuros)
        {
            MontantEuros = montantEuros;
        }

        public void ChangeClasse(ClasseLignePayeDelegant classe)
        {
            Classe = classe;
        }
    }
}
