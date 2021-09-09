using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("FacturesPayeesAuDelegant")]
    public class FacturePayeeAuDelegantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFacturePayeeAuDelegant { get; set; }
        public int IdPayeDelegant { get; set; }
        public int IdFacturation { get; set; }
        public int IdAbonné { get; set; }

        public FacturePayeeAuDelegantModel() { }
        public FacturePayeeAuDelegantModel(int idFacturePayeeAuDelegant, int idPayeDelegant, int idFacturation, int idAbonné) {
            IdFacturePayeeAuDelegant = idFacturePayeeAuDelegant;
            IdPayeDelegant = idPayeDelegant;
            IdFacturation = idFacturation;
            IdAbonné = idAbonné;
        }

    }
}
