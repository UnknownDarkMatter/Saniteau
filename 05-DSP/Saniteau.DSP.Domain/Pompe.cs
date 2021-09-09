using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain
{
    public class Pompe
    {
        public IdPompe IdPompe { get; private set; }
        public IdCompteur IdCompteur { get; private set; }
        public ChampLibre NuméroPompe { get; private set; }
        public Pompe(IdPompe idPompe, IdCompteur idCompteur, ChampLibre numéroPompe)
        {
            if (idPompe is null) { throw new ArgumentNullException(nameof(idPompe)); }
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }
            if (numéroPompe is null) { throw new ArgumentNullException(nameof(numéroPompe)); }

            IdPompe = idPompe;
            IdCompteur = idCompteur;
            NuméroPompe = numéroPompe;
        }

    }
}
