using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("FacturationLignes")]
    public class FacturationLigneModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFacturationLigne { get; set; }

        [ForeignKey("IdFacturation")]
        public virtual FacturationModel Facturation { get; set; }
        public int IdFacturation { get; set; }
        public ClasseLigneFacturation ClasseLigneFacturation { get; set; }
        public decimal MontantEuros { get; set; }
        public int ConsommationM3 { get; set; }
        public decimal PrixM3 { get; set; }

        public FacturationLigneModel() { }

        public FacturationLigneModel(int idFacturationLigne, int idFacturation, ClasseLigneFacturation classeLigneFacturation, decimal montantEuros, int consommationM3, decimal prixM3)
        {
            IdFacturationLigne = idFacturationLigne;
            IdFacturation = idFacturation;
            ClasseLigneFacturation = classeLigneFacturation;
            MontantEuros = montantEuros;
            ConsommationM3 = consommationM3;
            PrixM3 = prixM3;
        }
    }
}
