using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public interface RéférentielPaiement
    {
        List<Paiement> GetPaiements(IdFacturation idFacturation);
        Paiement EnregistrerPaiement(Paiement paiement);
    }
}
