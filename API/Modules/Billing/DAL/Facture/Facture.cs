using API.Modules.Orders.DAL.Commande;

namespace API.Modules.Billing.DAL.Facture;

using Paiement;

public partial class Facture
{
    public int InvoiceId { get; set; }

    public int? OrderId { get; set; }

    public decimal? MontantTotal { get; set; }

    public DateTime? DateFacture { get; set; }

    public virtual Commande? Order { get; set; }

    public virtual ICollection<Paiement> Paiements { get; set; } = [];
}
