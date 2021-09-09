using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("Abonnés")]
    public class AbonnéModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAbonné { get; set; }
        public int IdAdresse { get; set; }
        public string? Nom { get; set; }
        public string? Prénom { get; set; }
        public Tarification Tarification { get; set; }

        public AbonnéModel() { }

        public AbonnéModel(int idAbonné, int idAdresse, string? nom, string? prénom, Tarification tarification)
        {
            IdAbonné = idAbonné;
            IdAdresse = idAdresse;
            Nom = nom;
            Prénom = prénom;
            Tarification = tarification;
        }

    }
}
