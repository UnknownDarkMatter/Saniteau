using Saniteau.DSP.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Application.Mappers
{
    public static class ClasseLignePayeDelegantMapper
    {
        public static ClasseLignePayeDelegant Map(Contract.Model.ClasseLignePayeDelegant classe)
        {
            switch (classe)
            {
                case Contract.Model.ClasseLignePayeDelegant.NonDéfini:
                    {
                        return ClasseLignePayeDelegant.NonDéfini;
                    }
                case Contract.Model.ClasseLignePayeDelegant.FacturationAbonnés:
                    {
                        return ClasseLignePayeDelegant.FacturationAbonnés;
                    }
                case Contract.Model.ClasseLignePayeDelegant.RéumnérationSaniteau:
                    {
                        return ClasseLignePayeDelegant.RéumnérationSaniteau;
                    }
                case Contract.Model.ClasseLignePayeDelegant.ConsommationFuites:
                    {
                        return ClasseLignePayeDelegant.ConsommationFuites;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classe));
                    }
            }
        }
        public static Contract.Model.ClasseLignePayeDelegant Map(ClasseLignePayeDelegant classe)
        {
            switch (classe)
            {
                case ClasseLignePayeDelegant.NonDéfini:
                    {
                        return Contract.Model.ClasseLignePayeDelegant.NonDéfini;
                    }
                case ClasseLignePayeDelegant.FacturationAbonnés:
                    {
                        return Contract.Model.ClasseLignePayeDelegant.FacturationAbonnés;
                    }
                case ClasseLignePayeDelegant.RéumnérationSaniteau:
                    {
                        return Contract.Model.ClasseLignePayeDelegant.RéumnérationSaniteau;
                    }
                case ClasseLignePayeDelegant.ConsommationFuites:
                    {
                        return Contract.Model.ClasseLignePayeDelegant.ConsommationFuites;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classe));
                    }
            }
        }
    }
}
