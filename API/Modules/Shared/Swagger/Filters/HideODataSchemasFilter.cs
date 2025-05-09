using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Modules.Shared.Swagger.Filters;

public class HideODataSchemasFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var schemasToRemove = swaggerDoc.Components.Schemas
            .Where(schema => schema.Key.StartsWith("IEdm") || schema.Key.StartsWith("OData"))
            .Select(schema => schema.Key)
            .ToList();

        foreach(var schema in schemasToRemove)
        {
            swaggerDoc.Components.Schemas.Remove(schema);
        }
    }
}
