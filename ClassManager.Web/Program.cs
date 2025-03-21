using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using ClassManager.Web;
using ClassManager.Web.Services.APIs;
using ClassManager.Web.Services.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<QuizAuthStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(sp => sp.GetRequiredService<QuizAuthStateProvider>());

builder.Services.AddAuthorizationCore();

ConfigureRefit(builder.Services);



await builder.Build().RunAsync();

static void ConfigureRefit(IServiceCollection services)
{
    const string ApiBaseUrl = "http://localhost:5018";
    services.AddRefitClient<IAuthApi>()
            .ConfigureHttpClient(SetHttpClient);

    services.AddRefitClient<IDepartmentApi>(GetReffitSettings)
                   .ConfigureHttpClient(SetHttpClient);

    services.AddRefitClient<IUserApi>(GetReffitSettings)
            .ConfigureHttpClient(SetHttpClient);

    services.AddRefitClient<IClassApi>(GetReffitSettings)
        .ConfigureHttpClient(SetHttpClient);

    static RefitSettings GetReffitSettings(IServiceProvider sp)
    {
        var authStateProvider = sp.GetRequiredService<QuizAuthStateProvider>();
        return new RefitSettings
        {
            AuthorizationHeaderValueGetter = (_, __) => Task.FromResult(authStateProvider.User?.Token ?? "")
        };
    }

    static void SetHttpClient(HttpClient httpClient) => httpClient.BaseAddress = new Uri(ApiBaseUrl);
}

