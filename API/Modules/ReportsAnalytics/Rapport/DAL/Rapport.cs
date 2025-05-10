namespace API.Modules.ReportsAnalytics.Rapport.DAL;

public partial class Rapport
{
    public int ReportId { get; set; }

    public string? Type { get; set; }

    public DateTime? DateCreation { get; set; }

    public string? Contenu { get; set; }
}
