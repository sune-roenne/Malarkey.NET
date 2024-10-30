﻿using Malarkey.Integration;
using Malarkey.Integration.Microsoft;
using Malarkey.UI.Pages;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace Malarkey.UI;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json");
        builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);
        return builder;
    }


    public static WebApplicationBuilder AddUiServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddMicrosoftIdentityConsentHandler();
        builder.Services.AddRazorPages()
            .AddMvcOptions(_ => { })
            .AddMicrosoftIdentityUI();
        builder.Services.AddAppEntraIdIdentityProvider(builder.Configuration);
        builder.Services.AddAuthenticatedAuthorizationPolicy();
        builder.Services.AddAntiforgery();
        builder.Services.AddCascadingAuthenticationState();
        return builder;
    }

    public static WebApplication UseUiServices(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        app.MapRazorPages();

        return app;
    }

}