namespace IndicinaDecideLibrary;

using System.Text.Json;

public enum Currency
{
    NGN,
    EGP,
    KES
}

public enum Bank
{
    ACCESS = 44,
    ALAT = 353,
    CIB = 818147,
    ECOBANK = 50,
    FBN = 11,
    FCMB = 214,
    FIDELITY = 70,
    GLOBUS = 103,
    GTB = 58,
    HSBC = 818039,
    KEYSTONE = 82,
    KUDA = 50211,
    MBS = 41,
    MPESA = 404001,
    PROVIDUS = 101,
    POLARIS = 76,
    STANBIC = 221,
    STERLING = 232,
    UBA = 33,
    UNITY = 215,
    UNION = 32,
    ZENITH = 57
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
