using API.Modules.Billing.DAL.Facture;
using API.Modules.Billing.DAL.Paiement;
using API.Modules.Orders.DAL.Commande;
using API.Modules.Orders.DAL.LigneCommande;
using API.Modules.Products.DAL.Categorie;
using API.Modules.Products.DAL.Fournisseur;
using API.Modules.Products.DAL.Inventaire;
using API.Modules.Products.DAL.Marque;
using API.Modules.Products.DAL.Produit;
using API.Modules.ReportsAnalytics.DAL.Rapport;
using Microsoft.EntityFrameworkCore;

namespace API.Modules;

public partial class GestionCommerceContext : DbContext
{
    public GestionCommerceContext()
    {
    }

    public GestionCommerceContext(DbContextOptions<GestionCommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<Facture> Factures { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Inventaire> Inventaires { get; set; }

    public virtual DbSet<LigneCommande> LigneCommandes { get; set; }

    public virtual DbSet<Marque> Marques { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Rapport> Rapports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=GestionCommerce;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategorieConfiguration());

        modelBuilder.ApplyConfiguration(new CommandeConfiiguration());

        modelBuilder.ApplyConfiguration(new FactureConfiguration());

        modelBuilder.ApplyConfiguration(new FournisseurConfiguration());

        modelBuilder.ApplyConfiguration(new InventaireConfiguration());

        modelBuilder.ApplyConfiguration(new LigneCommandeConfiguration());

        modelBuilder.ApplyConfiguration(new MarqueConfiguration());

        modelBuilder.ApplyConfiguration(new PaiementConfiguration());

        modelBuilder.ApplyConfiguration(new ProductConfiguration());

        modelBuilder.ApplyConfiguration(new RapportConfiguration());


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
