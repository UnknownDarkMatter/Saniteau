using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Mappers
{
    public static class TarificationMapper
    {
        public static Models.Abonnés.Tarification Map(Facturation.Contract.Model.Tarification tarification)
        {
            switch (tarification)
            {
                case Facturation.Contract.Model.Tarification.NonDéfini:
                    {
                        return Models.Abonnés.Tarification.NonDefini;
                    }
                case Facturation.Contract.Model.Tarification.Particulier:
                    {
                        return Models.Abonnés.Tarification.Particulier;
                    }
                case Facturation.Contract.Model.Tarification.Professionnel:
                    {
                        return Models.Abonnés.Tarification.Professionnel;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(tarification));
                    }
            }
        }
        public static Compteurs.Contract.Model.Tarification MapToCompteurs(Models.Abonnés.Tarification tarification)
        {
            switch (tarification)
            {
                case Models.Abonnés.Tarification.NonDefini:
                    {
                        return Compteurs.Contract.Model.Tarification.NonDéfini;
                    }
                case Models.Abonnés.Tarification.Particulier:
                    {
                        return Compteurs.Contract.Model.Tarification.Particulier;
                    }
                case Models.Abonnés.Tarification.Professionnel:
                    {
                        return Compteurs.Contract.Model.Tarification.Professionnel;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(tarification));
                    }
            }
        }
        public static Facturation.Contract.Model.Tarification MapToFacturation(Models.Abonnés.Tarification tarification)
        {
            switch (tarification)
            {
                case Models.Abonnés.Tarification.NonDefini:
                    {
                        return Facturation.Contract.Model.Tarification.NonDéfini;
                    }
                case Models.Abonnés.Tarification.Particulier:
                    {
                        return Facturation.Contract.Model.Tarification.Particulier;
                    }
                case Models.Abonnés.Tarification.Professionnel:
                    {
                        return Facturation.Contract.Model.Tarification.Professionnel;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(tarification));
                    }
            }
        }
    }
}
