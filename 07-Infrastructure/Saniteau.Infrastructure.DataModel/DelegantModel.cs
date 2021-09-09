using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saniteau.Infrastructure.DataModel
{
    [Table("Delegants")]
    public class DelegantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDelegant { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public DateTime DateContrat { get; set; }

        public DelegantModel() { }
        public DelegantModel(int idDelegant, string nom, string adresse, DateTime dateContrat)
        {
            IdDelegant = idDelegant;
            Nom = nom;
            Adresse = adresse;
            DateContrat = dateContrat;
        }
    }
}
