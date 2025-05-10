using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.Products.Marque.DAL;

public class MarqueConfiguration : IEntityTypeConfiguration<Marque>
{
    public void Configure(EntityTypeBuilder<Marque> entity)
    {
        entity.HasKey(e => e.MarqueId).HasName("PK__Marque__769D72D6F0C3A0F1");

        entity.ToTable("Marque");

        entity.Property(e => e.MarqueId)
            .ValueGeneratedNever()
            .HasColumnName("marque_id");
        entity.Property(e => e.Nom)
            .HasMaxLength(100)
            .HasColumnName("nom");

        entity.HasData(new Marque
        {
            MarqueId = 1,
            Nom = "Samsung"
        }, new Marque
        {
            MarqueId = 2,
            Nom = "Nike"
        }, new Marque
        {
            MarqueId = 3,
            Nom = "IKEA"
        });
    }
}
