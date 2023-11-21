using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace TheBeRealProject.Services;

public class OAuthAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly NavigationManager _navigationManager;
    private readonly IHttpClientFactory _factory;
    
    private Task<AuthenticationState> _getAuthenticationStateTask;
    public OAuthAuthenticationStateProvider(NavigationManager navigationManager, IHttpClientFactory factory)
    {
        _navigationManager = navigationManager;
        _factory = factory;
        CreateGetAuthenticationStateClass(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task SetAuthenticationStateASync(string code)
    {
        using var httpClient = _factory.CreateClient(nameof(OAuthAuthenticationStateProvider));
        using var content = new StringContent(JsonSerializer.Serialize(new
        {
            client_id = "4fbba8cccce03aef50c5",
            client_secret = "23cfd7b7601203d8dd665b5b8a5381f40dba36ac",
            code,
            accept = "json"
        }));
        using var response = await httpClient.PostAsJsonAsync("access_token", content).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var tokenContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var token = new JwtSecurityToken(tokenContent);
        CreateGetAuthenticationStateClass(new ClaimsPrincipal(new ClaimsIdentity(token.Claims)));
        NotifyAuthenticationStateChanged(_getAuthenticationStateTask);
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    => _getAuthenticationStateTask;

    private void CreateGetAuthenticationStateClass(ClaimsPrincipal principal)
    => _getAuthenticationStateTask = Task.FromResult(new AuthenticationState(principal));

}
