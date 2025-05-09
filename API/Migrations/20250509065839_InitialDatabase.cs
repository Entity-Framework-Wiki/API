using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    categorie_id = table.Column<int>(type: "int", nullable: false),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__55E5113F543E4EE1", x => x.categorie_id);
                });

            migrationBuilder.CreateTable(
                name: "Commande",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false),
                    date_commande = table.Column<DateTime>(type: "datetime", nullable: true),
                    statut = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Commande__465962294B266D76", x => x.order_id);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseur",
                columns: table => new
                {
                    fournisseur_id = table.Column<int>(type: "int", nullable: false),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    contact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Fourniss__BDBA7AF07EEDB774", x => x.fournisseur_id);
                });

            migrationBuilder.CreateTable(
                name: "Marque",
                columns: table => new
                {
                    marque_id = table.Column<int>(type: "int", nullable: false),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Marque__769D72D6F0C3A0F1", x => x.marque_id);
                });

            migrationBuilder.CreateTable(
                name: "Produit",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    categorie_id = table.Column<int>(type: "int", nullable: true),
                    marque_id = table.Column<int>(type: "int", nullable: true),
                    prix = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Produit__47027DF580514356", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "Rapport",
                columns: table => new
                {
                    report_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    date_creation = table.Column<DateTime>(type: "datetime", nullable: true),
                    contenu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rapport__779B7C584AB1FA31", x => x.report_id);
                });

            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    invoice_id = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    montant_total = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    date_facture = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Facture__F58DFD49F402E057", x => x.invoice_id);
                    table.ForeignKey(
                        name: "FK__Facture__order_i__34C8D9D1",
                        column: x => x.order_id,
                        principalTable: "Commande",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateTable(
                name: "Inventaire",
                columns: table => new
                {
                    inventory_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    stock = table.Column<int>(type: "int", nullable: true),
                    fournisseur_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventai__B59ACC492F30A110", x => x.inventory_id);
                    table.ForeignKey(
                        name: "FK__Inventair__produ__2A4B4B5E",
                        column: x => x.product_id,
                        principalTable: "Produit",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "LigneCommande",
                columns: table => new
                {
                    ligne_id = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    quantite = table.Column<int>(type: "int", nullable: true),
                    prix_unitaire = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LigneCom__CA8432CC6AE4D0D1", x => x.ligne_id);
                    table.ForeignKey(
                        name: "FK__LigneComm__order__30F848ED",
                        column: x => x.order_id,
                        principalTable: "Commande",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK__LigneComm__produ__31EC6D26",
                        column: x => x.product_id,
                        principalTable: "Produit",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "Paiement",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "int", nullable: false),
                    invoice_id = table.Column<int>(type: "int", nullable: true),
                    montant = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    date_paiement = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paiement__ED1FC9EADD00F57C", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK__Paiement__invoic__37A5467C",
                        column: x => x.invoice_id,
                        principalTable: "Facture",
                        principalColumn: "invoice_id");
                });

            migrationBuilder.InsertData(
                table: "Facture",
                columns: new[] { "invoice_id", "date_facture", "montant_total", "order_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1089.97m, 2 },
                    { 2, new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 299.99m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facture_order_id",
                table: "Facture",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventaire_product_id",
                table: "Inventaire",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommande_order_id",
                table: "LigneCommande",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommande_product_id",
                table: "LigneCommande",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_invoice_id",
                table: "Paiement",
                column: "invoice_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropTable(
                name: "Fournisseur");

            migrationBuilder.DropTable(
                name: "Inventaire");

            migrationBuilder.DropTable(
                name: "LigneCommande");

            migrationBuilder.DropTable(
                name: "Marque");

            migrationBuilder.DropTable(
                name: "Paiement");

            migrationBuilder.DropTable(
                name: "Rapport");

            migrationBuilder.DropTable(
                name: "Produit");

            migrationBuilder.DropTable(
                name: "Facture");

            migrationBuilder.DropTable(
                name: "Commande");
        }
    }
}
