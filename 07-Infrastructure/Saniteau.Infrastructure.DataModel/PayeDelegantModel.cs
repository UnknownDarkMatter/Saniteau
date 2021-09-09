using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("PayesDelegant")]
    public class PayeDelegantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPayeDelegant { get; set; }
        public int IdDelegant { get; set; }
        public DateTime DatePaye { get; set; }
        public virtual ICollection<PayeDelegantLigneModel> LignesPaye { get; set; }

        public PayeDelegantModel() { }
        public PayeDelegantModel(int idPayeDelegant, int idDelegant, DateTime datePaye, List<PayeDelegantLigneModel> lignesPaye)
        {
            IdPayeDelegant = idPayeDelegant;
            IdDelegant = idDelegant;
            DatePaye = datePaye;
            LignesPaye = lignesPaye;
        }
    }
}
