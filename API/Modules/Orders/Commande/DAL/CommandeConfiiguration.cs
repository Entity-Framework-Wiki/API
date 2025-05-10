using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.Orders.Commande.DAL;

public class CommandeConfiiguration : IEntityTypeConfiguration<Commande>
{
    public void Configure(EntityTypeBuilder<Commande> entity)
    {
        entity.HasKey(e => e.OrderId).HasName("PK__Commande__465962294B266D76");

        entity.ToTable("Commande");

        entity.Property(e => e.OrderId)
            .ValueGeneratedNever()
            .HasColumnName("order_id");
        entity.Property(e => e.DateCommande)
            .HasColumnType("datetime")
            .HasColumnName("date_commande");
        entity.Property(e => e.Statut)
            .HasMaxLength(50)
            .HasColumnName("statut");

        entity.HasData(new Commande
        {
            OrderId = 1,
            DateCommande = new DateTime(2025, 5, 1),
            Statut = "Livrée"
        }, new Commande
        {
            OrderId = 2,
            DateCommande = new DateTime(2025, 5, 3),
            Statut = "En cours"
        }, new Commande
        {
            OrderId = 3,
            DateCommande = new DateTime(2025, 5, 5),
            Statut = "Annulée"
        });
    }
}
