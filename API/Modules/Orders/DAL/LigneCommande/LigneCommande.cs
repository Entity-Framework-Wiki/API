using API.Modules.Products.DAL.Produit;

namespace API.Modules.Orders.DAL.LigneCommande;

using Commande;
using System.ComponentModel.DataAnnotations;

public partial class LigneCommande
{
    [Key]
    public int LigneId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantite { get; set; }

    public decimal? PrixUnitaire { get; set; }

    public virtual Commande? Order { get; set; }

    public virtual Produit? Product { get; set; }
}
