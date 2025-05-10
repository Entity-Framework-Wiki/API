namespace API.Modules.Billing.Facture.DAL;

using API.Modules.Orders.Commande.DAL;
using Paiement.DAL;

public partial class Facture
{
    public int InvoiceId { get; set; }

    public int? OrderId { get; set; }

    public decimal? MontantTotal { get; set; }

    public DateTime? DateFacture { get; set; }

    public virtual Commande? Order { get; set; }

    public virtual ICollection<Paiement> Paiements { get; set; } = [];
}
