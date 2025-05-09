using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.Products.DAL.Categorie;

public class CategorieConfiguration : IEntityTypeConfiguration<Categorie>
{
    public void Configure(EntityTypeBuilder<Categorie> entity)
    {
        entity.HasKey(e => e.CategorieId).HasName("PK__Categori__55E5113F543E4EE1");

        entity.ToTable("Categorie");

        entity.Property(e => e.CategorieId)
            .ValueGeneratedNever()
            .HasColumnName("categorie_id");
        entity.Property(e => e.Nom)
            .HasMaxLength(100)
            .HasColumnName("nom");

        entity.HasData(new Categorie
        {
            CategorieId = 1,
            Nom = "Électronique"
        }, new Categorie
        {
            CategorieId = 2,
            Nom = "Vêtements"
        }, new Categorie
        {
            CategorieId = 3,
            Nom = "Maison & Jardin"
        });
    }
}
