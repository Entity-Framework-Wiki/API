using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.Products.Produit.DAL;

public class ProductConfiguration : IEntityTypeConfiguration<Produit>
{
    public void Configure(EntityTypeBuilder<Produit> entity)
    {
        entity.HasKey(e => e.ProductId).HasName("PK__Produit__47027DF580514356");

        entity.ToTable("Produit");

        entity.Property(e => e.ProductId)
            .ValueGeneratedNever()
            .HasColumnName("product_id");
        entity.Property(e => e.CategorieId).HasColumnName("categorie_id");
        entity.Property(e => e.MarqueId).HasColumnName("marque_id");
        entity.Property(e => e.Nom)
            .HasMaxLength(100)
            .HasColumnName("nom");
        entity.Property(e => e.Prix)
            .HasColumnType("decimal(10, 2)")
            .HasColumnName("prix");

        entity.HasData(
            new Produit { ProductId = 1, Nom = "Chaussures de sport", CategorieId = 1, MarqueId = 1, Prix = (decimal?)499.99 },
            new Produit { ProductId = 2, Nom = "Télévision 4K", CategorieId = 2, MarqueId = 2, Prix = (decimal?)89.99 },
            new Produit { ProductId = 3, Nom = "Canapé 3 places", CategorieId = 3, MarqueId = 3, Prix = (decimal?)299.99 }
        );
    }
}
