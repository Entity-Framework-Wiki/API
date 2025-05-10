using Microsoft.OData.ModelBuilder;

namespace API.Modules.Products.Produit;

using Produit.DAL;

public static class ProduitEdmConfiguration
{
    public static ODataConventionModelBuilder ConfigureProduitEdm(this ODataConventionModelBuilder builder)
    {
        var produits = builder.EntitySet<Produit>("Produit");

        produits.EntityType.HasMany(p => p.LigneCommandes); // Déclarer comme navigation property

        return builder;
    }
}
