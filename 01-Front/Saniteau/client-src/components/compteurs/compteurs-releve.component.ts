import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CampagneReleve } from '../../model/releve/CampagneReleve';
import { AppService } from '../../services/AppService';
import { HttpService } from '../../services/HttpService';

@Component({
    selector: 'compteurs-releve',
    templateUrl: 'compteurs-releve.component.html',
    styleUrls: ['./compteurs-releve.component.css']
})

export class CompteursReleveComponent implements OnInit {

    public dataTableTableId: string = "campagnesTable";

    constructor(@Inject(AppService) public appService,
        @Inject(HttpService) public httpService: HttpService,
        @Inject(MatSnackBar) public snackBar: MatSnackBar) {
    }
    ngOnInit(): void {
        let observable = this.httpService.getAsObservable('Releves/ObtenirCampagnesReleve');
        observable.subscribe(data => {
            let campagnes = this.constructCampagnes(data as CampagneReleve[]);
            this.refreshDataTable(campagnes);
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    constructCampagnes(data: CampagneReleve[]) {
        let result: CampagneReleve[] = [];
        let campagnes = (data as CampagneReleve[]);
        for (var i = 0; i < campagnes.length; i++) {
            let campagne: CampagneReleve = campagnes[i];
            result.push(new CampagneReleve(campagne.idCampagneReleve, campagne.dateCampagne, campagne.consommationUsagersM3, campagne.consommationPompePrincipaleM3));
        }
        return result;
    }

    releve() {
        let observable = this.httpService.getAsObservable('Releves/LancerReleve');
        observable.subscribe(data => {
            this.refreshDataTable(this.constructCampagnes(data as CampagneReleve[]));
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    refreshDataTable(campagnes: CampagneReleve[]) {
        var language = {
            "emptyTable": "Aucune donnée disponible dans le tableau",
            "lengthMenu": "Afficher _MENU_ éléments",
            "loadingRecords": "Chargement...",
            "processing": "Traitement...",
            "zeroRecords": "Aucun élément correspondant trouvé",
            "paginate": {
                "first": "Premier",
                "last": "Dernier",
                "next": "Suivant",
                "previous": "Précédent"
            },
            "aria": {
                "sortAscending": ": activer pour trier la colonne par ordre croissant",
                "sortDescending": ": activer pour trier la colonne par ordre décroissant"
            },
            "select": {
                "rows": {
                    "_": "%d lignes sélectionnées",
                    "0": "Aucune ligne sélectionnée",
                    "1": "1 ligne sélectionnée"
                },
                "1": "1 ligne selectionnée",
                "_": "%d lignes selectionnées",
                "cells": {
                    "1": "1 cellule sélectionnée",
                    "_": "%d cellules sélectionnées"
                },
                "columns": {
                    "1": "1 colonne sélectionnée",
                    "_": "%d colonnes sélectionnées"
                }
            },
            "autoFill": {
                "cancel": "Annuler",
                "fill": "Remplir toutes les cellules avec <i>%d<\/i>",
                "fillHorizontal": "Remplir les cellules horizontalement",
                "fillVertical": "Remplir les cellules verticalement",
                "info": "Exemple de remplissage automatique"
            },
            "searchBuilder": {
                "conditions": {
                    "date": {
                        "after": "Après le",
                        "before": "Avant le",
                        "between": "Entre",
                        "empty": "Vide",
                        "equals": "Egal à",
                        "not": "Différent de",
                        "notBetween": "Pas entre",
                        "notEmpty": "Non vide"
                    },
                    "number": {
                        "between": "Entre",
                        "empty": "Vide",
                        "equals": "Egal à",
                        "gt": "Supérieur à",
                        "gte": "Supérieur ou égal à",
                        "lt": "Inférieur à",
                        "lte": "Inférieur ou égal à",
                        "not": "Différent de",
                        "notBetween": "Pas entre",
                        "notEmpty": "Non vide"
                    },
                    "string": {
                        "contains": "Contient",
                        "empty": "Vide",
                        "endsWith": "Se termine par",
                        "equals": "Egal à",
                        "not": "Différent de",
                        "notEmpty": "Non vide",
                        "startsWith": "Commence par"
                    },
                    "array": {
                        "equals": "Egal à",
                        "empty": "Vide",
                        "contains": "Contient",
                        "not": "Différent de",
                        "notEmpty": "Non vide",
                        "without": "Sans"
                    }
                },
                "add": "Ajouter une condition",
                "button": {
                    "0": "Recherche avancée",
                    "_": "Recherche avancée (%d)"
                },
                "clearAll": "Effacer tout",
                "condition": "Condition",
                "data": "Donnée",
                "deleteTitle": "Supprimer la règle de filtrage",
                "logicAnd": "Et",
                "logicOr": "Ou",
                "title": {
                    "0": "Recherche avancée",
                    "_": "Recherche avancée (%d)"
                },
                "value": "Valeur"
            },
            "searchPanes": {
                "clearMessage": "Effacer tout",
                "count": "{total}",
                "title": "Filtres actifs - %d",
                "collapse": {
                    "0": "Volet de recherche",
                    "_": "Volet de recherche (%d)"
                },
                "countFiltered": "{shown} ({total})",
                "emptyPanes": "Pas de volet de recherche",
                "loadMessage": "Chargement du volet de recherche..."
            },
            "buttons": {
                "copyKeys": "Appuyer sur ctrl ou u2318 + C pour copier les données du tableau dans votre presse-papier.",
                "collection": "Collection",
                "colvis": "Visibilité colonnes",
                "colvisRestore": "Rétablir visibilité",
                "copy": "Copier",
                "copySuccess": {
                    "1": "1 ligne copiée dans le presse-papier",
                    "_": "%ds lignes copiées dans le presse-papier"
                },
                "copyTitle": "Copier dans le presse-papier",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Afficher toutes les lignes",
                    "1": "Afficher 1 ligne",
                    "_": "Afficher %d lignes"
                },
                "pdf": "PDF",
                "print": "Imprimer"
            },
            "decimal": ",",
            "info": "Affichage de _START_ à _END_ sur _TOTAL_ éléments",
            "infoEmpty": "Affichage de 0 à 0 sur 0 éléments",
            "infoFiltered": "(filtrés de _MAX_ éléments au total)",
            "infoThousands": ".",
            "search": "Rechercher:",
            "searchPlaceholder": "...",
            "thousands": "."
        };
        var table = $('#' + this.dataTableTableId).DataTable({ "destroy": true, "language": language });
        table.clear();
        for (var i = 0; i < campagnes.length; i++) {
            let campagne = campagnes[i];
            table.row.add([campagne.idCampagneReleve, campagne.formatedDate, campagne.consommationUsagersM3, campagne.pourcentageFuites]).draw(false);
        }
    }


}
