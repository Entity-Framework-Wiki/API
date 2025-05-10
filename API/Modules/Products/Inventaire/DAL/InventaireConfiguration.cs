using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.Products.Inventaire.DAL;

public class InventaireConfiguration : IEntityTypeConfiguration<Inventaire>
{
    public void Configure(EntityTypeBuilder<Inventaire> entity)
    {
        entity.HasKey(e => e.InventoryId).HasName("PK__Inventai__B59ACC492F30A110");

        entity.ToTable("Inventaire");

        entity.Property(e => e.InventoryId)
            .ValueGeneratedNever()
            .HasColumnName("inventory_id");
        entity.Property(e => e.FournisseurId).HasColumnName("fournisseur_id");
        entity.Property(e => e.ProductId).HasColumnName("product_id");
        entity.Property(e => e.Stock).HasColumnName("stock");

        entity.HasOne(d => d.Product).WithMany(p => p.Inventaires)
            .HasForeignKey(d => d.ProductId)
            .HasConstraintName("FK__Inventair__produ__2A4B4B5E");

        entity.HasData(new Inventaire
        {
            InventoryId = 1,
            ProductId = 1,
            Stock = 100,
            FournisseurId = 1
        }, new Inventaire
        {
            InventoryId = 2,
            ProductId = 2,
            Stock = 200,
            FournisseurId = 2
        }, new Inventaire
        {
            InventoryId = 3,
            ProductId = 3,
            Stock = 50,
            FournisseurId = 3
        });
    }
}