using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Modules.ReportsAnalytics.Rapport.DAL;

public class RapportConfiguration : IEntityTypeConfiguration<Rapport>
{
    public void Configure(EntityTypeBuilder<Rapport> entity)
    {
        entity.HasKey(e => e.ReportId).HasName("PK__Rapport__779B7C584AB1FA31");

        entity.ToTable("Rapport");

        entity.Property(e => e.ReportId)
            .ValueGeneratedNever()
            .HasColumnName("report_id");
        entity.Property(e => e.Contenu).HasColumnName("contenu");
        entity.Property(e => e.DateCreation)
            .HasColumnType("datetime")
            .HasColumnName("date_creation");
        entity.Property(e => e.Type)
            .HasMaxLength(50)
            .HasColumnName("type");
    }
}
