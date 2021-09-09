using Saniteau.Facturation.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public static class TarificationMapper
    {
        public static Tarification Map(Contract.Model.Tarification tarification)
        {
            switch (tarification)
            {
                case Contract.Model.Tarification.NonDéfini:
                    {
                        return Tarification.NonDéfini;
                    }
                case Contract.Model.Tarification.Particulier:
                    {
                        return Tarification.Particulier;
                    }
                case Contract.Model.Tarification.Professionnel:
                    {
                        return Tarification.Professionnel;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(tarification));
                    }
            }
        }
        public static Contract.Model.Tarification Map(Tarification tarification)
        {
            switch (tarification)
            {
                case Tarification.NonDéfini:
                    {
                        return Contract.Model.Tarification.NonDéfini;
                    }
                case Tarification.Particulier:
                    {
                        return Contract.Model.Tarification.Particulier;
                    }
                case Tarification.Professionnel:
                    {
                        return Contract.Model.Tarification.Professionnel;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(tarification));
                    }
            }
        }
    }
}
