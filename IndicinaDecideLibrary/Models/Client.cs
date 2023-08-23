using System.Net;
using System.Net.Http.Headers;

namespace IndicinaDecideLibrary;

public interface IHttpClientService
{
    HttpResponseMessage PostAsync(string url, HttpContent content, string? authToken = null);
    HttpResponseMessage GetAsync(string url, string? authToken = null);
    HttpResponseMessage DeleteAsync(string url, string? authToken = null);
}


public class DefaultHttpClientService : IHttpClientService
{
    private readonly HttpClient _client;

    public DefaultHttpClientService()
    {
        _client = new HttpClient();
    }

    public HttpResponseMessage PostAsync(string url, HttpContent content, string? authToken = null)
    {
        if (!string.IsNullOrEmpty(authToken))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        return _client.PostAsync(url, content).Result;
    }

    public HttpResponseMessage GetAsync(string url, string? authToken = null)
    {
        if (!string.IsNullOrEmpty(authToken))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        return _client.GetAsync(url).Result;
    }

    public HttpResponseMessage DeleteAsync(string url, string? authToken = null)
    {
        if (!string.IsNullOrEmpty(authToken))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        return _client.DeleteAsync(url).Result;
    }
}


public class MockHttpClientService : IHttpClientService
{
    private readonly HttpStatusCode _statusCode;
    private readonly string _responseContent;

    public MockHttpClientService(HttpStatusCode statusCode = HttpStatusCode.OK, string responseContent = "{\"Data\": {\"Token\": \"fake_token\"}}")
    {
        _statusCode = statusCode;
        _responseContent = responseContent;
    }

    public HttpResponseMessage PostAsync(string url, HttpContent content, string? authToken)
    {
        // You can customize this response based on what you need for your tests
        if (url.Contains("client/pdf/extract"))
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"status\":\"success\",\"data\":{\"jobId\":\"7a07b5391921bed1d16fe96cfd1c69bb1978244161f9cbe0d75fd54db27a1422\",\"status\":\"IN_PROGRESS\"}}") // Sample JSON response
            };
        }
        return new HttpResponseMessage(_statusCode)
        {
            Content = new StringContent(_responseContent)
        };
    }

    public HttpResponseMessage GetAsync(string url, string? authToken)
    {
        return new HttpResponseMessage(_statusCode);
    }

    public HttpResponseMessage DeleteAsync(string url, string? authToken)
    {
        return new HttpResponseMessage(_statusCode);
    }
}
