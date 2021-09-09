using Saniteau.Compteurs.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Repository.Mappers
{
    public static class TarificationMapper
    {
        public static Tarification Map(Infrastructure.DataModel.Tarification tarification)
        {
            switch (tarification)
            {
                case Infrastructure.DataModel.Tarification.NonDéfini:
                    {
                        return Tarification.NonDéfini;
                    }
                case Infrastructure.DataModel.Tarification.Particulier:
                    {
                        return Tarification.Particulier;
                    }
                case Infrastructure.DataModel.Tarification.Professionnel:
                    {
                        return Tarification.Professionnel;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(tarification));
                    }
            }
        }
        public static Infrastructure.DataModel.Tarification Map(Tarification tarification)
        {
            switch (tarification)
            {
                case Tarification.NonDéfini:
                    {
                        return Infrastructure.DataModel.Tarification.NonDéfini;
                    }
                case Tarification.Particulier:
                    {
                        return Infrastructure.DataModel.Tarification.Particulier;
                    }
                case Tarification.Professionnel:
                    {
                        return Infrastructure.DataModel.Tarification.Professionnel;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(tarification));
                    }
            }
        }
    }
}
