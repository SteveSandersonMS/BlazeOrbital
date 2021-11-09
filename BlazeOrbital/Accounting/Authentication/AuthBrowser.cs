using IdentityModel.OidcClient.Browser;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BlazeOrbital.Accounting.Authentication;

public class AuthBrowser : IBrowser
{
    private BrowserOptions? _options;

    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        _options = options;

        var semaphoreSlim = new SemaphoreSlim(0, 1);
        var browserResult = new BrowserResult()
        {
            ResultType = BrowserResultType.UserCancel
        };

        var signinWindow = new Window()
        {
            Width = 800,
            Height = 600,
            Title = "Sign In",
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };
        signinWindow.Closing += (s, e) =>
        {
            semaphoreSlim.Release();
        };

        var webView = new WebView2();
        webView.NavigationStarting += (s, e) =>
        {
            if (IsBrowserNavigatingToRedirectUri(new Uri(e.Uri)))
            {
                e.Cancel = true;

                browserResult = new BrowserResult()
                {
                    ResultType = BrowserResultType.Success,
                    Response = new Uri(e.Uri).AbsoluteUri
                };

                semaphoreSlim.Release();
                signinWindow.Close();
            }
        };

        signinWindow.Content = webView;
        signinWindow.Show();

        // Initialization
        await webView.EnsureCoreWebView2Async(null);

        // Delete existing Cookies so previous logins won't remembered
        // webView.CoreWebView2.CookieManager.DeleteAllCookies();

        // Navigate
        webView.CoreWebView2.Navigate(_options.StartUrl);

        await semaphoreSlim.WaitAsync();

        return browserResult;
    }

    private bool IsBrowserNavigatingToRedirectUri(Uri uri)
    {
        return uri.AbsoluteUri.StartsWith(_options!.EndUrl);
    }
}
