using API.Modules.Billing.Endpoints;

namespace API.Modules.Billing;

public static class BillingModule
{
    public static IEndpointRouteBuilder MapBillingEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var billing = endpoints
            .MapGroup("Billing")
            .WithOpenApi()
            .WithTags("Billing");


        billing
            .MapGet("Paiements", PaiementEndpoints.Tests)
            .Produces(StatusCodes.Status200OK)
            .WithDescription(PaiementEndpoints.Description);

        return billing;
    }
}
