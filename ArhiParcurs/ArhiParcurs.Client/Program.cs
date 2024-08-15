using ArhiParcurs.Client;
using ArhiParcurs.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using System.Globalization;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXdedXRSR2ZYU0J0XkU=");
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(navigationManager.BaseUri)
    };
});
builder.Services.AddSyncfusionBlazor();

builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped<IFuelService,FuelService>();
builder.Services.AddScoped<ISheetRouteService,SheetRouteService>();
builder.Services.AddScoped<ISheetService,SheetService>();
builder.Services.AddScoped<IPartnerService,PartnerService>();
builder.Services.AddScoped<IConsumptionIncreaseService,ConsumptionIncreaseService>();
builder.Services.AddScoped<IVehicleService,VehicleService>();
builder.Services.AddScoped<SfDialogService>();
var host = builder.Build();
//Setting culture of the application
var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
var result = await jsInterop.InvokeAsync<string>("cultureInfo.get");
CultureInfo culture;
if (result != null)
{
    culture = new CultureInfo(result);
}
else
{
    culture = new CultureInfo("ro-RO");
    await jsInterop.InvokeVoidAsync("cultureInfo.set", "ro-RO");
}
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await builder.Build().RunAsync();


public class SyncfusionLocalizer : ISyncfusionStringLocalizer
{
    // To get the locale key from mapped resources file
    public string GetText(string key)
    {
        return this.ResourceManager.GetString(key);
    }

    // To access the resource file and get the exact value for locale key

    public System.Resources.ResourceManager ResourceManager
    {
        get
        {
            // Replace the ApplicationNamespace with your application name.
            return ArhiParcurs.Client.Resources.SfResources.ResourceManager;
        }
    }
}