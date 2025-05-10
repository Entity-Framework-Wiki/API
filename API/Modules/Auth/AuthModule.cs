using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Modules.Auth;

public static class AuthModule
{
    public static IServiceCollection RegisterAuthModule(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = "https://dev-6s6s0f4wpurx7gmw.eu.auth0.com/";
            options.Audience = "https://gestion-commerce.com";
        });

        return services;
    }

    public static WebApplication RegisterHttpPipelineAuth(this WebApplication app)
    {
        app.UseAuthentication();

        // Obligatoire même si utilisation uniquememnt pour l'autthenttification
        app.UseAuthorization();
        return app;
    }
}
