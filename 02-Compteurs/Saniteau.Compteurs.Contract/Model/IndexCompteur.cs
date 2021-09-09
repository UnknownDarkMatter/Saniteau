using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Model
{
    public class IndexCompteur
    {
        public int IdIndex { get; set; }
        public int IdCompteur { get;set; }
        public int IdCampagneReleve { get; set; }
        public int IndexM3 { get; set; }
        public DateTime DateIndex { get; set; }

        public IndexCompteur(int idIndex, int idCompteur, int idCampagneReleve, int indexM3, DateTime dateIndex)
        {
            IdIndex = idIndex;
            IdCompteur = idCompteur;
            IdCampagneReleve = idCampagneReleve;
            IndexM3 = indexM3;
            DateIndex = dateIndex;
        }
    }
}
