using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("AppairagesCompteurs")]
    public class AppairageCompteurModel
    {
        [Key]
        public int IdAppairageCompteur { get; set; }
        public int IdPDL { get; set; }
        public int? IdCompteur { get; set; }
        public DateTime? DateAppairage { get; set; }
        public DateTime? DateDésappairage { get; set; }

        public AppairageCompteurModel() {  }

        public AppairageCompteurModel(int idAppairageCompteur, int idPDL, int? idCompteur, DateTime? dateAppairage, DateTime? dateDésappairage)
        {
            IdAppairageCompteur = idAppairageCompteur;
            IdPDL = idPDL;
            IdCompteur = idCompteur;
            DateAppairage = dateAppairage;
            DateDésappairage = dateDésappairage;
        }
    }
}
