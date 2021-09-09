using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("Pompes")]
    public class PompeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPompe { get; set; }
        public int IdCompteur { get; set; }
        public string? NuméroPompe { get; set; }

        public PompeModel()
        {

        }

        public PompeModel(int idPompe, int idCompteur, string? numéroPompe)
        {
            IdPompe = idPompe;
            IdCompteur = idCompteur;
            NuméroPompe = numéroPompe;
        }
    }
}
