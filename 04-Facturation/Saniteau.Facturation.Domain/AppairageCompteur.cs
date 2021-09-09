using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Domain
{
    public class AppairageCompteur
    {
        public IdAppairageCompteur IdAppairageCompteur { get; private set; }
        public IdPDL IdPDL { get; private set; }
        public IdCompteur IdCompteur { get; private set; }
        public Date? DateAppairage { get; private set; }
        public Date? DateDésappairage { get; private set; }

        public AppairageCompteur(IdAppairageCompteur idAppairageCompteur, IdPDL idPDL)
        {
            if (idAppairageCompteur is null) { throw new ArgumentNullException(nameof(idAppairageCompteur)); }
            if (idPDL is null) { throw new ArgumentNullException(nameof(idPDL)); }

            IdAppairageCompteur = idAppairageCompteur;
            IdPDL = idPDL;
            IdCompteur = null;
            DateAppairage = null;
            DateDésappairage = null;
        }

        public AppairageCompteur(IdAppairageCompteur idAppairageCompteur, IdPDL idPDL, IdCompteur idCompteur, Date? dateAppairage, Date? dateDésappairage)
        {
            if (idAppairageCompteur is null) { throw new ArgumentNullException(nameof(idAppairageCompteur)); }
            if (idPDL is null) { throw new ArgumentNullException(nameof(idPDL)); }

            IdAppairageCompteur = idAppairageCompteur;
            IdPDL = idPDL;
            IdCompteur = idCompteur;
            DateAppairage = dateAppairage;
            DateDésappairage = dateDésappairage;
        }

        public void AppaireCompteur(IdCompteur idCompteur, RéférentielAppairage referentielAppairage, RéférentielCompteurs referentielCompteurs)
        {
            if (referentielAppairage is null) { throw new ArgumentNullException(nameof(referentielAppairage)); }
            if (referentielCompteurs is null) { throw new ArgumentNullException(nameof(referentielCompteurs)); }

            var compteur = referentielCompteurs.GetCompteur(idCompteur);
            if (!compteur.CompteurPosé)
            {
                throw new BusinessException("Impossible d'appairer un compteur non posé.");
            }

            var appairagesOfPDL = referentielAppairage.GetAppairageOfPDL(IdPDL);
            foreach (var appairage in appairagesOfPDL)
            {
                compteur = referentielCompteurs.GetCompteur(appairage.IdCompteur);
                if (compteur.CompteurPosé && EstAppairé())
                {
                    throw new BusinessException($"Impossible d'appairer un PDL qui a déjà un compteur d'appairé (Compteur déjà apparairé : {compteur.IdCompteur}, PDL : {IdPDL}).");
                }
            }

            IdCompteur = idCompteur;
            DateAppairage = Horloge.Instance.GetDate();
            DateDésappairage = null;
        }

        public void DesappaireCompteur()
        {
            IdCompteur = null;
            DateDésappairage = Horloge.Instance.GetDate();
        }

        public bool EstAppairé()
        {
            return DateAppairage != null && DateDésappairage == null;
        }
    }
}
