using ArhiParcurs.Client.Services;
using ArhiParcurs.Components;
using ArhiParcurs.Components.Account;
using ArhiParcurs.Data;
using ArhiParcurs.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Swashbuckle.AspNetCore.Filters;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXdedXRSR2ZYU0J0XkU=");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSyncfusionBlazor();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Fuel>("Fuels");
    builder.EntitySet<ConsumptionIncrease>("ConsumptionIncreases");
    builder.EntitySet<Partner>("Partners");
    builder.EntitySet<SheetRoute>("SheetRoutes");
    builder.EntitySet<Sheet>("Sheets");
    builder.EntitySet<Vehicle>("Vehicles");
    builder.EntitySet<Project>("Projects");
    builder.EntitySet<Provider>("Providers");
    builder.EntitySet<Setting>("Settings");
    builder.EntitySet<Branch>("Branches");


    var getFuel = builder.Function("calculateInitialFuel");
    getFuel.Parameter<int>("sheetId");
    getFuel.Parameter<int>("vehicleId");
    getFuel.Returns<decimal>();
    
    var recalculate = builder.Function("recalculate");
    recalculate.Parameter<int>("vehicleId");
    recalculate.Parameter<DateTime>("date");
    recalculate.Returns<bool>();
    
    var recalculateValidation = builder.Function("recalcValidation");
    recalculateValidation.Parameter<int>("vehicleId");
    recalculateValidation.Parameter<DateTime>("date");
    recalculateValidation.Returns<bool>();
    
    var getNumber = builder.Function("getLastNumber");
    getNumber.Returns<int>();

    //builder.EntitySet<FazFoaie>("FazFoi");
    //FunctionConfiguration myFirstFunction = datas.EntityType.Collection.Function("MyFirstFunction");
    //myFirstFunction.ReturnsCollectionFromEntitySet<TaskDatum>("Gantt");
    return builder.GetEdmModel();
}

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(navigationManager.BaseUri)
    };
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
builder.Services.AddScoped<IFuelService, FuelService>();
builder.Services.AddScoped<ISheetRouteService, SheetRouteService>();
builder.Services.AddScoped<ISheetService, SheetService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<IConsumptionIncreaseService, ConsumptionIncreaseService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddScoped<SfDialogService>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
}).AddOData(opt => 
    opt.AddRouteComponents("api", GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null)
    
);





var app = builder.Build();
//Setting culture of the application

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    c.EnablePersistAuthorization();
});
app.UseHttpsRedirection();
//app.UseRouting();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ArhiParcurs.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
