using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("AdressesPDL")]
    public class AdressePDLModel
    {
        [Key]
        public int IdAdressePDL { get; set; }
        public int IdAdresse { get; set; }
        public int IdPDL { get; set; }

        public AdressePDLModel() { }
        public AdressePDLModel(int idAdressePDL, int idAdresse, int idPDL)
        {
            IdAdressePDL = idAdressePDL;
            IdAdresse = idAdresse;
            IdPDL = idPDL;
        }

    }
}
