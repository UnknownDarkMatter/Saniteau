using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Saniteau.Compteurs.Domain
{
    public class IndexCompteur
    {

        public IdIndex IdIndex { get; private set; }
        public IdCompteur IdCompteur { get; private set; }
        public int IndexM3 { get; private set; }
        public int IdCampagneReleve { get; private set; }
        public DateTime DateIndex { get; private set; }
        public IndexCompteur(IdIndex idIndex, IdCompteur idCompteur, int indexM3, int idCampagneReleve, DateTime dateIndex)
        {
            if (idIndex is null) { throw new ArgumentNullException(nameof(idIndex)); }
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }

            IdIndex = idIndex;
            IdCompteur = idCompteur;
            IndexM3 = indexM3;
            IdCampagneReleve = idCampagneReleve;
            DateIndex = dateIndex;
        }


    }
}
