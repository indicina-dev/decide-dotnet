namespace IndicinaDecideLibrary;

using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

public class DecideAuth
{
    private string? _clientId;
    private string? _clientSecret;
    private string _accessToken;
    private DateTime _accessTokenExpiry;
    private readonly IHttpClientService _httpClientService;

    // Constructor for default behavior
    public DecideAuth(string clientId, string clientSecret)
        : this(clientId, clientSecret, new DefaultHttpClientService())
    {
    }

    // Constructor for custom behavior (e.g., testing)
    public DecideAuth(string clientId, string clientSecret, IHttpClientService httpClientService)
    {
        if (string.IsNullOrWhiteSpace(clientId))
        {
            throw new DecideException("Missing or empty client id");
        }
        if (string.IsNullOrWhiteSpace(clientSecret))
        {
            throw new DecideException("Missing or empty client secret");
        }
        _clientId = clientId;
        _clientSecret = clientSecret;
        _httpClientService = httpClientService ?? throw new ArgumentNullException(nameof(httpClientService));
        _accessToken = GenerateAccessToken();
    }

    private string GenerateAccessToken()
    {
        try
        {
            string url = "https://api.indicina.co/api/v3/client/api/login";

            // Create the request body
            var requestBody = new
            {
                client_id = _clientId,
                client_secret = _clientSecret
            };

            // Convert the request body to JSON
            var jsonBody = JsonConvert.SerializeObject(requestBody);

            // Make the API call to generate the access token
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = _httpClientService.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // Handle the successful response
                var result = response.Content.ReadAsStringAsync().Result;
                var authorizationResponse = JsonConvert.DeserializeObject<AuthorizationResponse>(result);

                // Check if the authorization response and token are not null
                if (authorizationResponse != null && authorizationResponse.Data != null && authorizationResponse.Data.Token != null)
                {
                    // Set the access token expiry time (assumed 1 hour from now)
                    _accessTokenExpiry = DateTime.Now.AddHours(1);

                    return authorizationResponse.Data.Token;
                }
                else
                {
                    throw new DecideException("Invalid authorization response received.");
                }
            }
            else
            {
                // Handle the error response
                throw new DecideException("Error generating access token: " + response.ReasonPhrase);
            }
        }
        catch (Exception ex)
        {
            throw new DecideException("Error generating access token: " + ex.Message);
        }
    }

    private void RefreshAccessToken()
    {
        // Check if the access token has expired
        if (_accessTokenExpiry <= DateTime.Now)
        {
            // Generate a new access token
            _accessToken = GenerateAccessToken();
        }
    }

    public string GetAccessToken()
    {
        RefreshAccessToken();
        return _accessToken;
    }

    // Nested class for authorization response
    private class AuthorizationResponse
    {
        public string? Status { get; set; }
        public AuthorizationData? Data { get; set; }
    }

    // Nested class for authorization data
    private class AuthorizationData
    {
        public string? Token { get; set; }
    }
}


