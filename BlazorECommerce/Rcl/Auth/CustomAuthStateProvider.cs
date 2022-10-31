namespace BlazorECommerce.Rcl.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    public CustomAuthStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        HttpClient = httpClient;
        LocalStorageService = localStorageService;
    }

    public HttpClient HttpClient { get; }

    public ILocalStorageService LocalStorageService { get; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authToken = await LocalStorageService.GetItemAsStringAsync("authToken");

        var identity = new ClaimsIdentity();
        HttpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(authToken))
        {
            try
            {
                identity = new ClaimsIdentity(ParseClaimsFromJson(authToken), "jwt");
                HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", string.Empty));
            }
            catch (Exception ex)
            {
                await LocalStorageService.RemoveItemAsync("authToken");
                identity = new ClaimsIdentity();
                HttpClient.DefaultRequestHeaders.Authorization = null;
                Console.WriteLine(ex);
            }
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private static IEnumerable<Claim> ParseClaimsFromJson(string jwt)
    {
        var payload = jwt.Split(".")[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        var claims = keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty)) ??
                     Array.Empty<Claim>();

        return claims;

        static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
