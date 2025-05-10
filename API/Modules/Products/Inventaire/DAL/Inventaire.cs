namespace API.Modules.Products.Inventaire.DAL;

using API.Modules.Products.Produit.DAL;

public partial class Inventaire
{
    public int InventoryId { get; set; }

    public int? ProductId { get; set; }

    public int? Stock { get; set; }

    public int? FournisseurId { get; set; }

    public virtual Produit? Product { get; set; }

}
