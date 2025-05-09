using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.Products.DAL.Fournisseur;

public class FournisseurConfiguration : IEntityTypeConfiguration<Fournisseur>
{
    public void Configure(EntityTypeBuilder<Fournisseur> entity)
    {
        entity.HasKey(e => e.FournisseurId).HasName("PK__Fourniss__BDBA7AF07EEDB774");

        entity.ToTable("Fournisseur");

        entity.Property(e => e.FournisseurId)
            .ValueGeneratedNever()
            .HasColumnName("fournisseur_id");
        entity.Property(e => e.Contact)
            .HasMaxLength(100)
            .HasColumnName("contact");
        entity.Property(e => e.Nom)
            .HasMaxLength(100)
            .HasColumnName("nom");

        entity.HasData(new Fournisseur
        {
            FournisseurId = 1,
            Nom = "Fournisseur Électronique",
            Contact = "contact@fournisseur-electro.com"
        }, new Fournisseur
        {
            FournisseurId = 2,
            Nom = "Fournisseur Mode",
            Contact = "contact@fournisseur-mode.com"
        }, new Fournisseur
        {
            FournisseurId = 3,
            Nom = "Fournisseur Maison",
            Contact = "contact@fournisseur-maison.com"
        });
    }
}
