using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Domain.Calcul
{
    public class FacturationCalculator
    {
        public decimal GetPrixM3 (Tarification tarification)
        {
            switch (tarification)
            {
                case Tarification.Particulier:
                    {
                        return Constantes.PrixM3.Particulier;
                    }
                case Tarification.Professionnel:
                    {
                        return Constantes.PrixM3.Professionnel;
                    }
                default: {
                        throw new ArgumentOutOfRangeException(nameof(tarification));
                    }
            }
        }
        public decimal GetAbonnement(Tarification tarification)
        {
            switch (tarification)
            {
                case Tarification.Particulier:
                    {
                        return Constantes.Abonnement.Particulier;
                    }
                case Tarification.Professionnel:
                    {
                        return Constantes.Abonnement.Professionnel;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(tarification));
                    }
            }
        }

    }
}
