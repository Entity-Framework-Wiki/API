using API.Modules.Products.Produit;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace API.Modules;

public static class OdataConfiguration
{
    public static IMvcBuilder RegisterOdata(this IMvcBuilder builder)
    {
        builder.AddOData(options =>
        {
            options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel());
        });

        return builder;
    }

    private static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder()
            .ConfigureProduitEdm();

        return builder.GetEdmModel();
    }
}
