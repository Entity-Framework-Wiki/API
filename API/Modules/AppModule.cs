using API.Modules.Auth;
using API.Modules.Products;
using Microsoft.OpenApi.Models;

namespace API.Modules;

public static class AppModule
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {

        builder.Services.AddDbContext<GestionCommerceContext>();

        builder.Services.AddEndpointsApiExplorer();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API Documentation", Version = "v1" });
        });

        // MVC
        builder.Services.AddControllers()
            .RegisterOdata()
            .RegisterProductsControllers();

        builder.Services.RegisterAuthModule();


        return builder;
    }


    public static WebApplication RegisterHttpPipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // MVC 
        app.UseRouting();

        app.RegisterHttpPipelineAuth();

        return app;
    }


    public static WebApplication MapEndpoints(this WebApplication app)
    {
        // MVC
        app.UseEndpoints(endpoints =>
        {
            _ = endpoints.MapControllers();
        });

        // Api Minimal
        // app.MapBillingEndpoints();

        return app;
    }
}
