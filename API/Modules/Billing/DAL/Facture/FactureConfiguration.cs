using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.Billing.DAL.Facture;

public class FactureConfiguration : IEntityTypeConfiguration<Facture>
{
    public void Configure(EntityTypeBuilder<Facture> entity)
    {
        entity.HasKey(e => e.InvoiceId).HasName("PK__Facture__F58DFD49F402E057");

        entity.ToTable("Facture");

        entity.Property(e => e.InvoiceId)
            .ValueGeneratedNever()
            .HasColumnName("invoice_id");
        entity.Property(e => e.DateFacture)
            .HasColumnType("datetime")
            .HasColumnName("date_facture");
        entity.Property(e => e.MontantTotal)
            .HasColumnType("decimal(10, 2)")
            .HasColumnName("montant_total");
        entity.Property(e => e.OrderId).HasColumnName("order_id");

        entity.HasOne(d => d.Order).WithMany(p => p.Factures)
            .HasForeignKey(d => d.OrderId)
            .HasConstraintName("FK__Facture__order_i__34C8D9D1");

        entity.HasData(new Facture
        {
            InvoiceId = 1,
            OrderId = 1,
            MontantTotal = (decimal?)1089.97,
            DateFacture = new DateTime(2025, 5, 2)
        }, new Facture
        {
            InvoiceId = 2,
            OrderId = 2,
            MontantTotal = (decimal?)299.99,
            DateFacture = new DateTime(2025, 5, 4)
        });

    }
}
