namespace IndicinaDecideLibrary;

using System.Text.Json;

public enum Currency
{
    NGN,
    EGP,
    KES
}

public static class Bank
{
    public const string ACCESS = "044";
    public const string ALAT = "035A";
    public const string CIB = "818147";
    public const string ECOBANK = "050";
    public const string FBN = "011";
    public const string FCMB = "214";
    public const string FIDELITY = "070";
    public const string GLOBUS = "00103";
    public const string GTB = "058";
    public const string HERITAGE = "030";
    public const string HSBC = "818039";
    public const string KEYSTONE = "082";
    public const string KUDA = "50211";
    public const string MBS = "041";
    public const string MPESA = "404001";
    public const string PROVIDUS = "101";
    public const string POLARIS = "076";
    public const string STANBIC = "221";
    public const string STERLING = "232";
    public const string UBA = "033";
    public const string UNITY = "215";
    public const string UNION = "032";
    public const string ZENITH = "057";
}

public static class BankExtensions
{
    private const string BANK_LIST_URL = "https://api.indicina.co/api/v3/banks";

    public static async Task<List<(string, string)>> GetBankList()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(BANK_LIST_URL);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                var deserializedResponse = JsonSerializer.Deserialize<dynamic>(responseContent);
                var bankList = deserializedResponse?["data"];

                if (bankList != null)
                {
                    var result = new List<(string, string)>();
                    foreach (var bank in bankList)
                    {
                        string name = bank["name"];
                        string code = bank["code"];
                        result.Add((name, code));
                    }
                    return result;
                }
                else
                {
                    throw new DecideException("Invalid JSON response received.");
                }
            }
            else
            {
                throw new DecideException($"Failed to get bank list. Status code: {response.StatusCode}");
            }
        }
    }
}

public enum StatementType
{
    JSON,
    CSV,
    PDF
}

public enum StatementFormat
{
    CUSTOM,
    MBS,
    MONO,
    OKRA
}

public enum PDFStatus
{
    DONE,
    FAILED,
    IN_PROGRESS
}
