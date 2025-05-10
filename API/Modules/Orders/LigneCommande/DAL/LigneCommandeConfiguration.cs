using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.Orders.LigneCommande.DAL;

public class LigneCommandeConfiguration : IEntityTypeConfiguration<LigneCommande>
{
    public void Configure(EntityTypeBuilder<LigneCommande> entity)
    {
        entity.HasKey(e => e.LigneId).HasName("PK__LigneCom__CA8432CC6AE4D0D1");

        entity.ToTable("LigneCommande");

        entity.Property(e => e.LigneId)
            .ValueGeneratedNever()
            .HasColumnName("ligne_id");
        entity.Property(e => e.OrderId).HasColumnName("order_id");
        entity.Property(e => e.PrixUnitaire)
            .HasColumnType("decimal(10, 2)")
            .HasColumnName("prix_unitaire");
        entity.Property(e => e.ProductId).HasColumnName("product_id");
        entity.Property(e => e.Quantite).HasColumnName("quantite");

        entity.HasOne(d => d.Order).WithMany(p => p.LigneCommandes)
            .HasForeignKey(d => d.OrderId)
            .HasConstraintName("FK__LigneComm__order__30F848ED");

        entity.HasOne(d => d.Product).WithMany(p => p.LigneCommandes)
            .HasForeignKey(d => d.ProductId)
            .HasConstraintName("FK__LigneComm__produ__31EC6D26");

        entity.HasData(new LigneCommande
        {
            LigneId = 1,
            OrderId = 1,
            ProductId = 1,
            Quantite = 2,
            PrixUnitaire = (decimal?)499.99
        }, new LigneCommande
        {
            LigneId = 2,
            OrderId = 1,
            ProductId = 2,
            Quantite = 1,
            PrixUnitaire = (decimal?)89.99
        }, new LigneCommande
        {
            LigneId = 3,
            OrderId = 2,
            ProductId = 3,
            Quantite = 1,
            PrixUnitaire = (decimal?)299.99
        });
    }
}
