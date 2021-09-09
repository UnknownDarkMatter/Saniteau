using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{ 
    public interface RéférentielIndexesCompteurs
    {
        List<IndexCompteur> GetIndexesOfCompteur(IdCompteur idCompteur);
    }
}
