using BlazeOrbital.Accounting.Authentication;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace BlazeOrbital.Accounting;

public class WpfAppAccessTokenProvider : IAccessTokenProvider
{
    public string BackendUrl { get; } = "https://localhost:7087/";
    public static readonly WpfAppAccessTokenProvider Instance = new();

    private Task<AccessTokenResult>? _latestLoginFlow;

    public async ValueTask<AccessTokenResult> RequestAccessToken()
        => await (_latestLoginFlow ??= RequestAccessTokenCore());

    public HttpMessageHandler CreateMessageHandler(IServiceProvider services)
    {
        var result = new AuthorizationMessageHandler(Instance, services.GetRequiredService<NavigationManager>());
        result.ConfigureHandler(authorizedUrls: new[] { BackendUrl });
        result.InnerHandler = new HttpClientHandler();
        return result;
    }

    public async Task LogOutAsync()
    {
        // This simply deletes the cookies from the embedded webview. It's not performing single sign-out.
        var webView = new WebView2();
        var window = new Window { Content = webView };
        window.Show();
        await webView.EnsureCoreWebView2Async(null);
        webView.CoreWebView2.CookieManager.DeleteAllCookies();
        window.Close();
        _latestLoginFlow = null;

        MessageBox.Show($"Logged out successfully", "Logged out", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private async Task<AccessTokenResult> RequestAccessTokenCore()
    {
        var oidcClient = new OidcClient(new OidcClientOptions
        {
            Authority = BackendUrl,
            ClientId = "BlazeOrbital.Accounting",
            Scope = "openid profile BlazeOrbital.CentralServerAPI",
            RedirectUri = $"{BackendUrl}authentication/login-callback",
            Browser = new AuthBrowser(),
            Policy = new Policy { RequireIdentityTokenSignature = false }
        });

        var loginResult = await oidcClient.LoginAsync();
        if (loginResult.IsError)
        {
            _latestLoginFlow = null;
            throw new InvalidOperationException($"Invalid access token result: '{loginResult.Error}'");
        }

        return new AccessTokenResult(AccessTokenResultStatus.Success, new AccessToken
        {
            Value = loginResult.AccessToken,
            Expires = loginResult.AccessTokenExpiration,
            GrantedScopes = loginResult.TokenResponse.Scope.Split(' '),
        }, null);
    }

    public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
        => throw new NotImplementedException();
}
