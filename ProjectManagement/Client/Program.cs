using Blazorise;
using Blazorise.Extensions;
using ProjectManagement.Client;
using ProjectManagement.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TabBlazor;
using TabBlazor.Services;
using Blazorise.Bootstrap;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        builder.Services.AddOptions();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<CustomStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ProjectManagement.Client.Services.INotificationService, NotificationService>();
        builder.Services.AddTabBlazor();
        builder.Services.AddTabler();
        builder.Services.AddBlazorise();
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddBootstrapProviders();
        await builder.Build().RunAsync();
    }
}