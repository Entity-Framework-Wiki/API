using API.Modules.Orders.DAL.LigneCommande;

namespace API.Modules.Products.DAL.Produit;

using Inventaire;
using System.ComponentModel.DataAnnotations;

public partial class Produit
{
    [Key]
    public int ProductId { get; set; }

    public string? Nom { get; set; }

    public int? CategorieId { get; set; }

    public int? MarqueId { get; set; }

    public decimal? Prix { get; set; }

    public virtual ICollection<Inventaire> Inventaires { get; set; } = new List<Inventaire>();

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();
}
