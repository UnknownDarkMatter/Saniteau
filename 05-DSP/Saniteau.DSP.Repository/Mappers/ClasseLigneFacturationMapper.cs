using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Repository.Mappers
{
    public static class ClasseLigneFacturationMapper
    {
        public static Domain.ClasseLigneFacturation Map(Infrastructure.DataModel.ClasseLigneFacturation classeLigneFacturation)
        {
            switch (classeLigneFacturation)
            {
                case Infrastructure.DataModel.ClasseLigneFacturation.NonDéfini:
                    {
                        return Domain.ClasseLigneFacturation.NonDéfini;
                    }
                case Infrastructure.DataModel.ClasseLigneFacturation.Abonnement:
                    {
                        return Domain.ClasseLigneFacturation.Abonnement;
                    }
                case Infrastructure.DataModel.ClasseLigneFacturation.ConsommationRéelle:
                    {
                        return Domain.ClasseLigneFacturation.ConsommationRéelle;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classeLigneFacturation));
                    }
            }
        }
        public static Infrastructure.DataModel.ClasseLigneFacturation Map(Domain.ClasseLigneFacturation classeLigneFacturation)
        {
            switch (classeLigneFacturation)
            {
                case Domain.ClasseLigneFacturation.NonDéfini:
                    {
                        return Infrastructure.DataModel.ClasseLigneFacturation.NonDéfini;
                    }
                case Domain.ClasseLigneFacturation.Abonnement:
                    {
                        return Infrastructure.DataModel.ClasseLigneFacturation.Abonnement;
                    }
                case Domain.ClasseLigneFacturation.ConsommationRéelle:
                    {
                        return Infrastructure.DataModel.ClasseLigneFacturation.ConsommationRéelle;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(classeLigneFacturation));
                    }
            }
        }

    }
}
