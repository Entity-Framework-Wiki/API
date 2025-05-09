using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.Billing.DAL.Paiement;

public class PaiementConfiguration : IEntityTypeConfiguration<Paiement>
{
    public void Configure(EntityTypeBuilder<Paiement> entity)
    {
        entity.HasKey(e => e.PaymentId).HasName("PK__Paiement__ED1FC9EADD00F57C");

        entity.ToTable("Paiement");

        entity.Property(e => e.PaymentId)
            .ValueGeneratedNever()
            .HasColumnName("payment_id");
        entity.Property(e => e.DatePaiement)
            .HasColumnType("datetime")
            .HasColumnName("date_paiement");
        entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
        entity.Property(e => e.Montant)
            .HasColumnType("decimal(10, 2)")
            .HasColumnName("montant");

        entity.HasOne(d => d.Invoice).WithMany(p => p.Paiements)
            .HasForeignKey(d => d.InvoiceId)
            .HasConstraintName("FK__Paiement__invoic__37A5467C");

        entity.HasData(new Paiement
        {
            PaymentId = 1,
            DatePaiement = new DateTime(2025, 5, 3),
            InvoiceId = 1,
            Montant = (decimal?)1089.97
        }, new Paiement
        {
            PaymentId = 2,
            DatePaiement = new DateTime(2025, 5, 5),
            InvoiceId = 2,
            Montant = (decimal?)299.99
        });

    }
}
