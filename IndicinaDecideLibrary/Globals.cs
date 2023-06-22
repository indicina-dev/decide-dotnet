namespace IndicinaDecideLibrary;

public static class Global
{
    public const string LIBRARY = "decide";
    public const string API_VERSION = "v3";

    public const string BASE_URL = "https://api.indicina.co/api/" + API_VERSION + "/";
    public const string LOGIN_URL = "https://api.indicina.co/api/v3/client/api/login";

    public const int MAX_RETRIES = 3;
}
