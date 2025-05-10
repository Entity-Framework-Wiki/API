using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace API.Modules.Products.Produit;

using Produit.DAL;

[Authorize]
[Route("api/[controller]")]
public class ProduitController(GestionCommerceContext context) : ODataController
{
    private readonly GestionCommerceContext _context = context;

    [Route("odata/[controller]")]
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Get(ODataQueryOptions<Produit> options)
    {
        IQueryable<Produit> query = _context.Produits;

        var result = options.ApplyTo(query);


        return this.Ok(result);
    }
}
