using API.Modules.Products.Endpoints;

namespace API.Modules.Products;

public static class ProductsModule
{
    public static IMvcBuilder RegisterProductsControllers(this IMvcBuilder builder)
    {
        builder.AddApplicationPart(typeof(ProduitController).Assembly);

        return builder;
    }

}
