using Saniteau.Common.Mediator;
using Saniteau.Compteurs.Contract.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Contract.Commands
{
    public class AppairageCommand : IAction<AppairageCompteur>
    {
        public int IdAppairageCompteur { get; set; }
        public int IdAdresse { get; set; }
        public int IdCompteur { get; set; }
        public int IdPDL { get; set; }

        public AppairageCommand(int idAppairageCompteur, int idAdresse, int idCompteur, int idPDL)
        {
            IdAppairageCompteur = idAppairageCompteur;
            IdAdresse = idAdresse;
            IdCompteur = idCompteur;
            IdPDL = idPDL;
        }

    }
}
