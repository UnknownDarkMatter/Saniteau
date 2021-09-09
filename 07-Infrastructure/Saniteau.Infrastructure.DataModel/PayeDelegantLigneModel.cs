using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("PayesDelegantLignes")]
    public class PayeDelegantLigneModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPayeDelegantLigne { get; set; }
        public ClasseLignePayeDelegant Classe { get; set; }
        public decimal MontantEuros { get; set; }

        [ForeignKey("IdPayeDelegant")]
        public virtual PayeDelegantModel PayeDelegant { get; set; }
        public int IdPayeDelegant { get; set; }

        public PayeDelegantLigneModel() { }
        public PayeDelegantLigneModel(int idPayeDelegantLigne, int idPayeDelegant, ClasseLignePayeDelegant classe, decimal montantEuros)
        {
            IdPayeDelegantLigne = idPayeDelegantLigne;
            IdPayeDelegant = idPayeDelegant;
            Classe = classe;
            MontantEuros = montantEuros;
        }

    }
}
