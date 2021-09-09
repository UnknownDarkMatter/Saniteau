using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saniteau.Infrastructure.DataContext.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abonnés",
                columns: table => new
                {
                    IdAbonné = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAdresse = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prénom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnés", x => x.IdAbonné);
                });

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    IdAdresse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NuméroEtRue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodePostal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.IdAdresse);
                });

            migrationBuilder.CreateTable(
                name: "AdressesPDL",
                columns: table => new
                {
                    IdAdressePDL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAdresse = table.Column<int>(type: "int", nullable: false),
                    IdPDL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdressesPDL", x => x.IdAdressePDL);
                });

            migrationBuilder.CreateTable(
                name: "AppairagesCompteurs",
                columns: table => new
                {
                    IdAppairageCompteur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPDL = table.Column<int>(type: "int", nullable: false),
                    IdCompteur = table.Column<int>(type: "int", nullable: true),
                    DateAppairage = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDésappairage = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppairagesCompteurs", x => x.IdAppairageCompteur);
                });

            migrationBuilder.CreateTable(
                name: "Compteurs",
                columns: table => new
                {
                    IdCompteur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NuméroCompteur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompteurPosé = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compteurs", x => x.IdCompteur);
                });

            migrationBuilder.CreateTable(
                name: "Delegants",
                columns: table => new
                {
                    IdDelegant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateContrat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delegants", x => x.IdDelegant);
                });

            migrationBuilder.CreateTable(
                name: "FacturesPayeesAuDelegant",
                columns: table => new
                {
                    IdFacturePayeeAuDelegant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPayeDelegant = table.Column<int>(type: "int", nullable: false),
                    IdFacturation = table.Column<int>(type: "int", nullable: false),
                    IdAbonné = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturesPayeesAuDelegant", x => x.IdFacturePayeeAuDelegant);
                });

            migrationBuilder.CreateTable(
                name: "IndexesCompteur",
                columns: table => new
                {
                    IdIndex = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompteur = table.Column<int>(type: "int", nullable: false),
                    IdCampagneReleve = table.Column<int>(type: "int", nullable: false),
                    IndexM3 = table.Column<int>(type: "int", nullable: false),
                    DateIndex = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexesCompteur", x => x.IdIndex);
                });

            migrationBuilder.CreateTable(
                name: "IndexesPayésParDelegant",
                columns: table => new
                {
                    IdIndexPayéParDelegant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPayeDelegant = table.Column<int>(type: "int", nullable: false),
                    IdCompteur = table.Column<int>(type: "int", nullable: false),
                    IdIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexesPayésParDelegant", x => x.IdIndexPayéParDelegant);
                });

            migrationBuilder.CreateTable(
                name: "PayesDelegant",
                columns: table => new
                {
                    IdPayeDelegant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDelegant = table.Column<int>(type: "int", nullable: false),
                    DatePaye = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayesDelegant", x => x.IdPayeDelegant);
                });

            migrationBuilder.CreateTable(
                name: "PDL",
                columns: table => new
                {
                    IdPDL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NuméroPDL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDL", x => x.IdPDL);
                });

            migrationBuilder.CreateTable(
                name: "Pompes",
                columns: table => new
                {
                    IdPompe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompteur = table.Column<int>(type: "int", nullable: false),
                    NuméroPompe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pompes", x => x.IdPompe);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facturations",
                columns: table => new
                {
                    IdFacturation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCampagneFacturation = table.Column<int>(type: "int", nullable: false),
                    IdAbonné = table.Column<int>(type: "int", nullable: false),
                    DateFacturation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDernierIndex = table.Column<int>(type: "int", nullable: false),
                    Payée = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturations", x => x.IdFacturation);
                    table.ForeignKey(
                        name: "FK_Facturations_Abonnés_IdAbonné",
                        column: x => x.IdAbonné,
                        principalTable: "Abonnés",
                        principalColumn: "IdAbonné",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayesDelegantLignes",
                columns: table => new
                {
                    IdPayeDelegantLigne = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Classe = table.Column<int>(type: "int", nullable: false),
                    MontantEuros = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdPayeDelegant = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayesDelegantLignes", x => x.IdPayeDelegantLigne);
                    table.ForeignKey(
                        name: "FK_PayesDelegantLignes_PayesDelegant_IdPayeDelegant",
                        column: x => x.IdPayeDelegant,
                        principalTable: "PayesDelegant",
                        principalColumn: "IdPayeDelegant",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacturationLignes",
                columns: table => new
                {
                    IdFacturationLigne = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFacturation = table.Column<int>(type: "int", nullable: false),
                    ClasseLigneFacturation = table.Column<int>(type: "int", nullable: false),
                    MontantEuros = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsommationM3 = table.Column<int>(type: "int", nullable: false),
                    PrixM3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturationLignes", x => x.IdFacturationLigne);
                    table.ForeignKey(
                        name: "FK_FacturationLignes_Facturations_IdFacturation",
                        column: x => x.IdFacturation,
                        principalTable: "Facturations",
                        principalColumn: "IdFacturation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturationLignes_IdFacturation",
                table: "FacturationLignes",
                column: "IdFacturation");

            migrationBuilder.CreateIndex(
                name: "IX_Facturations_IdAbonné",
                table: "Facturations",
                column: "IdAbonné");

            migrationBuilder.CreateIndex(
                name: "IX_PayesDelegantLignes_IdPayeDelegant",
                table: "PayesDelegantLignes",
                column: "IdPayeDelegant");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "AdressesPDL");

            migrationBuilder.DropTable(
                name: "AppairagesCompteurs");

            migrationBuilder.DropTable(
                name: "Compteurs");

            migrationBuilder.DropTable(
                name: "Delegants");

            migrationBuilder.DropTable(
                name: "FacturationLignes");

            migrationBuilder.DropTable(
                name: "FacturesPayeesAuDelegant");

            migrationBuilder.DropTable(
                name: "IndexesCompteur");

            migrationBuilder.DropTable(
                name: "IndexesPayésParDelegant");

            migrationBuilder.DropTable(
                name: "PayesDelegantLignes");

            migrationBuilder.DropTable(
                name: "PDL");

            migrationBuilder.DropTable(
                name: "Pompes");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Facturations");

            migrationBuilder.DropTable(
                name: "PayesDelegant");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Abonnés");
        }
    }
}
