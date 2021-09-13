using Saniteau.Helpers;
using Saniteau.Mappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Saniteau.PdfRendering
{
    public class PdfFactureGenerator
    {
        private const string LigneFactureKeyWord = "factu-element=\"ligne-facturation\"";

        public byte[] GeneratePdfFacture(Models.Facturation.Facturation facturation)
        {
            string html = GetHtmlOfFacturation(facturation);
            using (MemoryStream stream = new MemoryStream())
            {
                iText.Html2pdf.HtmlConverter.ConvertToPdf(html, stream);
                return stream.ToArray();
            }
        }
        private string GetHtmlOfFacturation(Models.Facturation.Facturation facturation)
        {
            var cultureInfo = new CultureInfo("fr-FR");
            string template = RessourcesHelper.GetRessource("FactureAbonne.html");
            template = template.Replace("{{numero-facture}}", facturation.IdFacturation.ToString());
            template = template.Replace("{{est-payee}}", facturation.Payee ? "Payée" : "Non payée");
            template = template.Replace("{{date-courante}}", DateTime.Now.ToString("dddd dd MMMM yyyy"));
            template = template.Replace("{{date-factu}}", facturation.DateFacturation.ToString("dddd dd MMMM yyyy"));
            template = template.Replace("{{prenom-nom}}", $"{facturation.Abonne.Prenom} {facturation.Abonne.Nom.ToUpper()}");
            template = template.Replace("{{adresse}}", facturation.Abonne.NumeroEtRue);
            template = template.Replace("{{codepostal-ville}}", $"{facturation.Abonne.CodePostal} {facturation.Abonne.Ville.ToUpper()}");
            template = template.Replace("{{ligne-facturation-total}}", facturation.FacturationLignes.Sum(m => m.MontantEuros).ToString(cultureInfo) + "€");

            int positionInsertionligneFacturation = template.IndexOf("</tr>", template.IndexOf(LigneFactureKeyWord)) + "</tr>".Length;
            string templateDebut = template.Substring(0, positionInsertionligneFacturation);
            string templateFin = template.Substring(positionInsertionligneFacturation);

            string templatePrestation = GetPrestationTemplate(template);
            string prestations = "";
            foreach (var ligneFacturation in facturation.FacturationLignes)
            {
                string templatePrestationTmp = templatePrestation;
                templatePrestationTmp = templatePrestationTmp.Replace("{{ligne-facturation-prestation}}", FacturationMapper.Map(ligneFacturation.ClasseLigneFacturation));
                templatePrestationTmp = templatePrestationTmp.Replace("{{ligne-facturation-montant}}", ligneFacturation.MontantEuros.ToString(cultureInfo) + "€");
                switch (ligneFacturation.ClasseLigneFacturation)
                {
                    case Models.Facturation.ClasseLigneFacturation.ConsommationReelle:
                        {
                            templatePrestationTmp = templatePrestationTmp.Replace(
                                "{{ligne-facturation-consomme}}", 
                                " de " + ligneFacturation.ConsommationM3.ToString() + " mètres cubes d'eau");
                            break;
                        }
                    default:
                        {
                            templatePrestationTmp = templatePrestationTmp.Replace("{{ligne-facturation-consomme}}", "");
                            break;
                        }
                }
                prestations += templatePrestationTmp;
            }
            template = $"{templateDebut}{prestations}{templateFin}";
            return template;
        }


        private string GetPrestationTemplate(string template)
        {
            var regex = new Regex($"\\<tr(?<debut>[^\\<]*)({LigneFactureKeyWord})(?<fin>.*)\\</tr\\>", RegexOptions.Singleline);
            var match = regex.Match(template);
            bool success = match.Success;
            string templatePrestation = "<tr" + match.Groups["debut"].Value + match.Groups["fin"].Value + "</tr>";
            templatePrestation = templatePrestation.Substring(0, templatePrestation.IndexOf("</tr>") + "</tr>".Length);
            templatePrestation = templatePrestation.Replace(";display:none", "");
            templatePrestation = templatePrestation.Replace("display:none;", "");
            templatePrestation = templatePrestation.Replace("display:none", "");
            return templatePrestation;
        }
    }
}
