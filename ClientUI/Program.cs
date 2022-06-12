using Blazored.LocalStorage;
using Client.Authentication;
using ClientUI;
using ClientUI.Services;
using ClientUI.Services.Cars;
using ClientUI.Services.Clients;
using ClientUI.Services.Emails;
using ClientUI.Services.Notifications;
using ClientUI.Services.Rides;
using ClientUI.Services.Roles;
using ClientUI.Services.Statuses;
using ClientUI.Utils;
using IntelligentCarManagement.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Syncfusion.Blazor;

// Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjM3OTQ5QDMyMzAyZTMxMmUzMENBajhkWVJBNjBrRlZjRkUvNDNGb3NoTXZKVlRnaU9OazhCblptOUFUQWM9");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = false; });

/* Custom services here */
builder.Services.AddCustomServices();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
