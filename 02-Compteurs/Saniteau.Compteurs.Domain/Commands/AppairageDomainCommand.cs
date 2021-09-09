using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Compteurs.Domain.Commands
{
    public class AppairageDomainCommand
    {
        public IdAppairageCompteur IdAppairageCompteur { get; private set; }
        public IdAdresse IdAdresse { get; private set; }
        public IdCompteur IdCompteur { get; private set; }
        public IdPDL IdPDL { get; private set; }

        public AppairageDomainCommand(IdAppairageCompteur idAppairageCompteur, IdAdresse idAdresse, IdCompteur idCompteur, IdPDL idPDL)
        {
            if (idAppairageCompteur is null) { throw new ArgumentNullException(nameof(idAppairageCompteur)); }
            if (idAdresse is null) { throw new ArgumentNullException(nameof(idAdresse)); }
            if (idCompteur is null) { throw new ArgumentNullException(nameof(idCompteur)); }
            if (idPDL is null) { throw new ArgumentNullException(nameof(idPDL)); }

            IdAppairageCompteur = idAppairageCompteur;
            IdAdresse = idAdresse;
            IdCompteur = idCompteur;
            IdPDL = idPDL;
        }

        public AppairageCompteur AppairageCompteur(RéférentielCompteurs référentielCompteurs, RéférentielPDL référentielPDL, RéférentielAbonnés référentielAbonnés, RéférentielAppairage référentielAppairage)
        {
            Adresse adresse = référentielAbonnés.GetAddresse(IdAdresse);
            if(adresse is null) { throw new BusinessException($"Impossible d'apparairer un compteur à une adresse inexistante en base (IdAdresse : {IdAdresse}, IdCompteur : {IdCompteur}, IdPDL: {IdPDL})."); }

            PDL pdl = référentielPDL.GetPDL(IdPDL);
            if (pdl is null) { throw new BusinessException($"Impossible d'apparairer un compteur à un PDL inexistant en base (IdAdresse : {IdAdresse}, IdCompteur : {IdCompteur}, IdPDL: {IdPDL})."); }

            Compteur compteur = référentielCompteurs.GetCompteur(IdCompteur);
            if (compteur is null) { throw new BusinessException($"Impossible d'apparairer un compteur s'il n'existe pas en base (IdAdresse : {IdAdresse}, IdCompteur : {IdCompteur}, IdPDL: {IdPDL})."); }

            référentielAppairage.SupprimeAdressePDL(IdAdresse);
            référentielAppairage.CréeAdressePDL(new AdressePDL(IdAdressePDL.Parse(0), IdAdresse, IdPDL));
            var appairage = new AppairageCompteur(IdAppairageCompteur, IdPDL);
            appairage.AppaireCompteur(IdCompteur, référentielAppairage, référentielCompteurs);
            appairage = référentielAppairage.EnregistreAppairageCompteur(appairage);

            return appairage;
        }
    }
}
