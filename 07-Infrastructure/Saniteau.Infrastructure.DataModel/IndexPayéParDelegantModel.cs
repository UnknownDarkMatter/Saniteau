using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("IndexesPayésParDelegant")]
    public class IndexPayéParDelegantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdIndexPayéParDelegant { get; set; }
        public int IdPayeDelegant { get; set; }
        public int IdCompteur { get; set; }
        public int IdIndex { get; set; }

        public IndexPayéParDelegantModel() { }
        public IndexPayéParDelegantModel(int idIndexPayéParDelegant, int idPayeDelegant, int idCompteur, int idIndex)
        {
            IdIndexPayéParDelegant = idIndexPayéParDelegant;
            IdPayeDelegant = idPayeDelegant;
            IdCompteur = idCompteur;
            IdIndex = idIndex;
        }

    }
}
