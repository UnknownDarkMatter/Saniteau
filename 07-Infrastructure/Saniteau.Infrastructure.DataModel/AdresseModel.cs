using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("Adresses")]
    public class AdresseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAdresse { get; set; }
        public string? NuméroEtRue { get; set; }
        public string? Ville { get; set; }
        public string? CodePostal { get;  set; }

        public AdresseModel() { }

        public AdresseModel(int idAdresse, string? numéroEtRue, string? ville, string? codePostal)
        {
            IdAdresse = idAdresse;
            NuméroEtRue = numéroEtRue;
            Ville = ville;
            CodePostal = codePostal;
        }

    }
}
