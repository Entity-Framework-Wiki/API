using API.Modules.Billing.DAL.Facture;

namespace API.Modules.Orders.DAL.Commande;

using LigneCommande;

public partial class Commande
{
    public int OrderId { get; set; }

    public DateTime? DateCommande { get; set; }

    public string? Statut { get; set; }

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();

}
