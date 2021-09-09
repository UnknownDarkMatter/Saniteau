using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("IndexesCompteur")]
    public class IndexCompteurModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdIndex { get; set; }
        public int IdCompteur { get; set; }
        public int IdCampagneReleve { get; set; }
        public int IndexM3 { get; set; }
        public DateTime DateIndex { get; set; }

        public IndexCompteurModel() {  }

        public IndexCompteurModel(int idIndex, int idCompteur, int idCampagneReleve, int indexM3, DateTime dateIndex)
        {
            IdIndex = idIndex;
            IdCompteur = idCompteur;
            IdCampagneReleve = idCampagneReleve;
            IndexM3 = indexM3;
            DateIndex = dateIndex;
        }

    }
}
