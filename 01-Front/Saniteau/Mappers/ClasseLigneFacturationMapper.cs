using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Mappers
{
    public static class ClasseLigneFacturationMapper
    {
        public static Models.Facturation.ClasseLigneFacturation Map(Facturation.Contract.Model.ClasseLigneFacturation classe)
        {
            switch (classe)
            {
                case Facturation.Contract.Model.ClasseLigneFacturation.NonDéfini:
                    {
                        return Models.Facturation.ClasseLigneFacturation.NonDefini;
                    }
                case Facturation.Contract.Model.ClasseLigneFacturation.Abonnement:
                    {
                        return Models.Facturation.ClasseLigneFacturation.Abonnement;
                    }
                case Facturation.Contract.Model.ClasseLigneFacturation.ConsommationRéelle:
                    {
                        return Models.Facturation.ClasseLigneFacturation.ConsommationReelle;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classe));
                    }
            }
        }
        public static Facturation.Contract.Model.ClasseLigneFacturation Map(Models.Facturation.ClasseLigneFacturation classe)
        {
            switch (classe)
            {
                case Models.Facturation.ClasseLigneFacturation.NonDefini:
                    {
                        return Facturation.Contract.Model.ClasseLigneFacturation.NonDéfini;
                    }
                case Models.Facturation.ClasseLigneFacturation.Abonnement:
                    {
                        return Facturation.Contract.Model.ClasseLigneFacturation.Abonnement;
                    }
                case Models.Facturation.ClasseLigneFacturation.ConsommationReelle:
                    {
                        return Facturation.Contract.Model.ClasseLigneFacturation.ConsommationRéelle;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classe));
                    }
            }
        }
    }
}
