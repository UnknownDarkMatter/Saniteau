using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Releves.Domain
{
    public class IndexCompteur
    {

        public IdIndex IdIndex { get; private set; }
        public IdCompteur IdCompteur { get; private set; }
        public int IdCampagneReleve { get; private set; }
        public int IndexM3 { get; private set; }
        public DateTime DateIndex { get; private set; }
        public IndexCompteur(IdIndex idIndex, IdCompteur idCompteur, int idCampagneReleve, int indexM3, DateTime dateIndex)
        {
            if (idIndex is null) { throw new ArgumentNullException(nameof(idIndex)); }
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }

            IdIndex = idIndex;
            IdCompteur = idCompteur;
            IdCampagneReleve = idCampagneReleve;
            IndexM3 = indexM3;
            DateIndex = dateIndex;
        }

        public void SetIndex(int newIndex, DateTime dateIndex)
        {
            IndexM3 = newIndex;
            DateIndex = dateIndex;
        }
    }
}
