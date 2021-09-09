using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("Facturations")]
    public class FacturationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFacturation { get; set; }

        public int IdCampagneFacturation { get; set; }

        [ForeignKey("IdAbonné")]
        public virtual AbonnéModel Abonné { get; set; }
        public int IdAbonné { get; set; }
        public DateTime DateFacturation { get; set; }
        public virtual ICollection<FacturationLigneModel> FacturationLignes { get; set; }
        public int IdDernierIndex { get; set; }
        public bool Payée { get; set; }

        public FacturationModel() { }

        public FacturationModel(int idFacturation, int idCampagneFacturation, int idAbonné, DateTime dateFacturation, int idDernierIndex, List<FacturationLigneModel> facturationLignes, bool payée)
        {
            IdFacturation = idFacturation;
            IdCampagneFacturation = idCampagneFacturation;
            IdAbonné = idAbonné;
            DateFacturation = dateFacturation;
            IdDernierIndex = idDernierIndex;
            FacturationLignes = facturationLignes;
            Payée = payée;
        }
    }
}
