using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorie",
                columns: new[] { "categorie_id", "nom" },
                values: new object[,]
                {
                    { 1, "Électronique" },
                    { 2, "Vêtements" },
                    { 3, "Maison & Jardin" }
                });

            migrationBuilder.InsertData(
                table: "Commande",
                columns: new[] { "order_id", "date_commande", "statut" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Livrée" },
                    { 2, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "En cours" },
                    { 3, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annulée" }
                });

            migrationBuilder.InsertData(
                table: "Facture",
                columns: new[] { "invoice_id", "order_id", "montant_total", "date_facture" },
                values: new object[,]
                {
                    { 1, 1, 1089.97m, new DateTime(2025, 5, 2) },
                    { 2, 2, 299.99m, new DateTime(2025, 5, 4) }
                });

            migrationBuilder.InsertData(
                table: "Fournisseur",
                columns: new[] { "fournisseur_id", "contact", "nom" },
                values: new object[,]
                {
                    { 1, "contact@fournisseur-electro.com", "Fournisseur Électronique" },
                    { 2, "contact@fournisseur-mode.com", "Fournisseur Mode" },
                    { 3, "contact@fournisseur-maison.com", "Fournisseur Maison" }
                });

            migrationBuilder.InsertData(
                table: "Marque",
                columns: new[] { "marque_id", "nom" },
                values: new object[,]
                {
                    { 1, "Samsung" },
                    { 2, "Nike" },
                    { 3, "IKEA" }
                });

            migrationBuilder.InsertData(
                table: "Paiement",
                columns: new[] { "payment_id", "date_paiement", "invoice_id", "montant" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1089.97m },
                    { 2, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 299.99m }
                });

            migrationBuilder.InsertData(
                table: "Produit",
                columns: new[] { "product_id", "categorie_id", "marque_id", "nom", "prix" },
                values: new object[,]
                {
                    { 1, 1, 1, "Chaussures de sport", 499.99m },
                    { 2, 2, 2, "Télévision 4K", 89.99m },
                    { 3, 3, 3, "Canapé 3 places", 299.99m }
                });

            migrationBuilder.InsertData(
                table: "Inventaire",
                columns: new[] { "inventory_id", "fournisseur_id", "product_id", "stock" },
                values: new object[,]
                {
                    { 1, 1, 1, 100 },
                    { 2, 2, 2, 200 },
                    { 3, 3, 3, 50 }
                });

            migrationBuilder.InsertData(
                table: "LigneCommande",
                columns: new[] { "ligne_id", "order_id", "prix_unitaire", "product_id", "quantite" },
                values: new object[,]
                {
                    { 1, 1, 499.99m, 1, 2 },
                    { 2, 1, 89.99m, 2, 1 },
                    { 3, 2, 299.99m, 3, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "categorie_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "categorie_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorie",
                keyColumn: "categorie_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Commande",
                keyColumn: "order_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Fournisseur",
                keyColumn: "fournisseur_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Fournisseur",
                keyColumn: "fournisseur_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Fournisseur",
                keyColumn: "fournisseur_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Inventaire",
                keyColumn: "inventory_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Inventaire",
                keyColumn: "inventory_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Inventaire",
                keyColumn: "inventory_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LigneCommande",
                keyColumn: "ligne_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LigneCommande",
                keyColumn: "ligne_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LigneCommande",
                keyColumn: "ligne_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Marque",
                keyColumn: "marque_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Marque",
                keyColumn: "marque_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Marque",
                keyColumn: "marque_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Paiement",
                keyColumn: "payment_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Paiement",
                keyColumn: "payment_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Commande",
                keyColumn: "order_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Commande",
                keyColumn: "order_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produit",
                keyColumn: "product_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Produit",
                keyColumn: "product_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Produit",
                keyColumn: "product_id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Facture",
                keyColumn: "invoice_id",
                keyValue: 1,
                column: "order_id",
                value: 2);
        }
    }
}
