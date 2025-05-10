using API.Modules.Billing.Facture.DAL;

namespace API.Modules.Orders.Commande.DAL;

using LigneCommande.DAL;

public partial class Commande
{
    public int OrderId { get; set; }

    public DateTime? DateCommande { get; set; }

    public string? Statut { get; set; }

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();

}
