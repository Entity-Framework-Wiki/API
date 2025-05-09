namespace API.Modules.Billing.DAL.Paiement;

using Facture;


public partial class Paiement
{
    public int PaymentId { get; set; }

    public int? InvoiceId { get; set; }

    public decimal? Montant { get; set; }

    public DateTime? DatePaiement { get; set; }

    public virtual Facture? Invoice { get; set; }
}
