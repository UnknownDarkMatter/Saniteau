using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("PDL")]
    public class PDLModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPDL { get; set; }
        public string? NuméroPDL { get; set; }

        public PDLModel()
        {

        }

        public PDLModel(int idPDL, string? numéroPDL)
        {
            IdPDL = idPDL;
            NuméroPDL = numéroPDL;
        }
    }
}
