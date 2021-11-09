using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace BlazeOrbital.ManufacturingHub.Data;

public class OfflineAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public const string OfflineClaimsKey = "offlineClaimsCache";
    private readonly IJSRuntime js;

    public OfflineAccountClaimsPrincipalFactory(IJSRuntime js, IAccessTokenProviderAccessor accessor) : base(accessor)
    {
        this.js = js;
    }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var result = await base.CreateUserAsync(account, options);
        if (result.Identity!.IsAuthenticated)
        {
            await js.InvokeVoidAsync("localStorage.setItem", OfflineClaimsKey, JsonSerializer.Serialize(result.Claims.Select(c => new ClaimData { Type = c.Type, Value = c.Value })));
        }
        else if (await js.InvokeAsync<string?>("localStorage.getItem", OfflineClaimsKey) is string cachedClaimsJson)
        {
            var cachedClaims = JsonSerializer.Deserialize<ClaimData[]>(cachedClaimsJson)!;
            result = new ClaimsPrincipal(new ClaimsIdentity(cachedClaims.Select(c => new Claim(c.Type!, c.Value!)), "BlazeOrbital.ManufacturingHub", nameType: "name", roleType: "role"));
        }

        return result;
    }

    class ClaimData
    {
        public string? Type { get; set; }
        public string? Value { get; set; }
    }
}
