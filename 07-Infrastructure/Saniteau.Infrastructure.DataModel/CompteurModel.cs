using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("Compteurs")]
    public class CompteurModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompteur { get; set; }
        public string NuméroCompteur { get; set; }
        public bool CompteurPosé { get; set; }

        public CompteurModel()
        {

        }
        public CompteurModel(int idCompteur, string numéroCompteur, bool compteurPosé)
        {
            IdCompteur = idCompteur;
            NuméroCompteur = numéroCompteur;
            CompteurPosé = compteurPosé;
        }
    }
}
