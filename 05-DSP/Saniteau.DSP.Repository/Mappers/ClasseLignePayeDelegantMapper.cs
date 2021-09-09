using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class ClasseLignePayeDelegantMapper
    {
        public static Domain.ClasseLignePayeDelegant Map(Infrastructure.DataModel.ClasseLignePayeDelegant classeLignePayeDelegant)
        {
            switch (classeLignePayeDelegant)
            {
                case Infrastructure.DataModel.ClasseLignePayeDelegant.NonDéfini:
                    {
                        return Domain.ClasseLignePayeDelegant.NonDéfini;
                    }
                case Infrastructure.DataModel.ClasseLignePayeDelegant.FacturationAbonnés:
                    {
                        return Domain.ClasseLignePayeDelegant.FacturationAbonnés;
                    }
                case Infrastructure.DataModel.ClasseLignePayeDelegant.RéumnérationSaniteau:
                    {
                        return Domain.ClasseLignePayeDelegant.RéumnérationSaniteau;
                    }
                case Infrastructure.DataModel.ClasseLignePayeDelegant.ConsommationFuites:
                    {
                        return Domain.ClasseLignePayeDelegant.ConsommationFuites;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classeLignePayeDelegant));
                    }
            }
        }
        public static Infrastructure.DataModel.ClasseLignePayeDelegant Map(Domain.ClasseLignePayeDelegant classeLignePayeDelegant)
        {
            switch (classeLignePayeDelegant)
            {
                case Domain.ClasseLignePayeDelegant.NonDéfini:
                    {
                        return Infrastructure.DataModel.ClasseLignePayeDelegant.NonDéfini;
                    }
                case Domain.ClasseLignePayeDelegant.FacturationAbonnés:
                    {
                        return Infrastructure.DataModel.ClasseLignePayeDelegant.FacturationAbonnés;
                    }
                case Domain.ClasseLignePayeDelegant.RéumnérationSaniteau:
                    {
                        return Infrastructure.DataModel.ClasseLignePayeDelegant.RéumnérationSaniteau;
                    }
                case Domain.ClasseLignePayeDelegant.ConsommationFuites:
                    {
                        return Infrastructure.DataModel.ClasseLignePayeDelegant.ConsommationFuites;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classeLignePayeDelegant));
                    }
            }
        }

    }
}
