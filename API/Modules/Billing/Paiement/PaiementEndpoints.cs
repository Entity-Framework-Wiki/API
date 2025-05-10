using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.Billing.Paiement;

public static class PaiementEndpoints
{
    public const string Description = "Test endpoints";

    public static async Task<IResult> Tests([FromServices] GestionCommerceContext dbContext)
    {
        var datas = await dbContext.Paiements.ToListAsync();

        return Results.Ok(datas);
    }
}
