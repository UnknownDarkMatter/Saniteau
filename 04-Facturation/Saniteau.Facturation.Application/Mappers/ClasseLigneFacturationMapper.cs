using Saniteau.Facturation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public static class ClasseLigneFacturationMapper
    {
        public static ClasseLigneFacturation Map(Contract.Model.ClasseLigneFacturation classe)
        {
            switch (classe)
            {
                case Contract.Model.ClasseLigneFacturation.NonDéfini:
                    {
                        return ClasseLigneFacturation.NonDéfini;
                    }
                case Contract.Model.ClasseLigneFacturation.Abonnement:
                    {
                        return ClasseLigneFacturation.Abonnement;
                    }
                case Contract.Model.ClasseLigneFacturation.ConsommationRéelle:
                    {
                        return ClasseLigneFacturation.ConsommationRéelle;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classe));
                    }
            }
        }
        public static Contract.Model.ClasseLigneFacturation Map(ClasseLigneFacturation classe)
        {
            switch (classe)
            {
                case ClasseLigneFacturation.NonDéfini:
                    {
                        return Contract.Model.ClasseLigneFacturation.NonDéfini;
                    }
                case ClasseLigneFacturation.Abonnement:
                    {
                        return Contract.Model.ClasseLigneFacturation.Abonnement;
                    }
                case ClasseLigneFacturation.ConsommationRéelle:
                    {
                        return Contract.Model.ClasseLigneFacturation.ConsommationRéelle;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classe));
                    }
            }
        }
    }
}
