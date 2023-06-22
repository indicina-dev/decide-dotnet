namespace IndicinaDecideLibrary;

using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

public class DecideAPI
{
    private Authorization _authorization;
    public DecideAPI()
    {
        _authorization = new Authorization();
    }

    public DefaultAnalysisResult AnalyzeJsonStatement(StatementFormat statementFormat, string statement, Customer customer, List<int>? scoreCardIds = null)
    {
        try
        {
            string url = "https://api.indicina.co/api/v3/client/bsp";

            // Deserialize the JSON string into a dictionary
            var statementDict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(statement);
            var bankStatement = new
            {
                type = statementFormat.ToString().ToLower(),
                content = statementDict
            };

            // Create the request body
            var requestBody = new
            {
                customer = customer.info,
                bankStatement = bankStatement,
                score_card_ids = scoreCardIds
            };

            // Convert the request body to JSON
            var jsonBody = System.Text.Json.JsonSerializer.Serialize(requestBody);

            // Create the HTTP client
            using (var client = new HttpClient())
            {
                // Set the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authorization.GetAccessToken());

                // Set the request content
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Make the API call to analyze the JSON statement
                var response = client.PostAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Handle the successful response
                    var result = response.Content.ReadAsStringAsync().Result;
                    var analysisResult = JsonConvert.DeserializeObject<DefaultAnalysisResult>(result);

                    // Check if the analysis result is not null
                    if (analysisResult != null)
                    {
                        return analysisResult;
                    }
                    else
                    {
                        throw new DecideException("Invalid analysis result received.");
                    }
                }
                else
                {
                    // Handle the error response
                    throw new DecideException("Error analyzing JSON statement: " + response.ReasonPhrase);
                }
            }
        }
        catch (Exception ex)
        {
            throw new DecideException("Error analyzing JSON statement: " + ex.Message);
        }
    }

    public DefaultAnalysisResult AnalyzeCsvStatement(string csvPath, Customer customer, List<int>? scoreCardIds = null)
    {
        try
        {
            string url = "https://api.indicina.co/api/v3/client/statement/analyze";

            // Create the request body
            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StreamContent(File.OpenRead(csvPath)), "csv", Path.GetFileName(csvPath));
            requestContent.Add(new StringContent(customer.customer_id), "customer_id");
            if (scoreCardIds != null && scoreCardIds.Count > 0)
            {
                foreach (var scoreCardId in scoreCardIds)
                {
                    requestContent.Add(new StringContent(scoreCardId.ToString()), "score_card_ids[]");
                }
            }

            // Create the HTTP client
            using (var client = new HttpClient())
            {
                // Set the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authorization.GetAccessToken());

                // Make the API call to analyze the CSV statement
                var response = client.PostAsync(url, requestContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Handle the successful response
                    var result = response.Content.ReadAsStringAsync().Result;
                    var analysisResult = JsonConvert.DeserializeObject<DefaultAnalysisResult>(result);

                    // Check if the analysis result is not null
                    if (analysisResult != null)
                    {
                        return analysisResult;
                    }
                    else
                    {
                        throw new DecideException("Invalid analysis result received.");
                    }
                }
                else
                {
                    // Handle the error response
                    throw new DecideException("Error analyzing JSON statement: " + response.ReasonPhrase);
                }
            }
        }
        catch (Exception ex)
        {
            throw new DecideException("Error analyzing CSV statement: " + ex.Message);
        }
    }

    public string InitiatePdfAnalysis(string pdfPath, Currency currency, Bank bank, Customer customer, string? pdfPassword = null, string requestType = "score", List<int>? scoreCardIds = null)
    {
        try
        {
            string url = "https://api.indicina.co/api/v3/client/pdf/extract";

            string fileName = pdfPath;
            byte[] fileBytes = File.ReadAllBytes(fileName);

            // Create multipart form data content
            var formDataContent = new MultipartFormDataContent();

            // Create byte array content for the file
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "pdf",
                FileName = Path.GetFileName(fileName)
            };

            // Create the request body
            formDataContent.Add(fileContent);
            formDataContent.Add(new StringContent(currency.ToString()), "currency");
            formDataContent.Add(new StringContent(((int)bank).ToString()), "bank_code");
            formDataContent.Add(new StringContent(customer.customer_id), "customer_id");
            formDataContent.Add(new StringContent(requestType), "request_type");

            if (pdfPassword != null)
            {
                formDataContent.Add(new StringContent(pdfPassword), "pdf_password");
            }

            if (scoreCardIds != null && scoreCardIds.Count > 0)
            {
                foreach (var scoreCardId in scoreCardIds)
                {
                    formDataContent.Add(new StringContent(scoreCardId.ToString()), "score_card_ids[]");
                }
            }

            // Create the HTTP client
            using (var client = new HttpClient())
            {
                // Set the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authorization.GetAccessToken());

                // Make the API call to initiate PDF analysis
                var response = client.PostAsync(url, formDataContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Handle the successful response
                    var result = response.Content.ReadAsStringAsync().Result;
                    var analysisResponse = JsonConvert.DeserializeObject<InitialPdfAnalysisResponse>(result);

                    // Check if the analysis response and job ID are not null
                    if (analysisResponse != null && analysisResponse.Data != null && !string.IsNullOrEmpty(analysisResponse.Data.JobId))
                    {
                        return analysisResponse.Data.JobId;
                    }
                    else
                    {
                        throw new DecideException("Invalid PDF analysis response received.");
                    }
                }
                else
                {
                    // Handle the error response
                    throw new DecideException("Error initiating PDF analysis: " + response.ReasonPhrase);
                }
            }
        }
        catch (Exception ex)
        {
            throw new DecideException("Error initiating PDF analysis: " + ex.Message);
        }
    }

    public PdfAnalysisResult GetPdfAnalysisResult(string jobId)
    {
        try
        {
            string url = $"https://api.indicina.co/api/v3/client/pdf/extract/{jobId}/status";

            // Create the HTTP client
            using (var client = new HttpClient())
            {
                // Set the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authorization.GetAccessToken());

                // Make the API call to get the PDF analysis result
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Handle the successful response
                    var result = response.Content.ReadAsStringAsync().Result;
                    var analysisResult = JsonConvert.DeserializeObject<PdfAnalysisResult>(result);

                    // Check if the analysis result is not null
                    if (analysisResult != null && analysisResult.Data != null)
                    {
                        return analysisResult;
                    }
                    else
                    {
                        throw new DecideException("Invalid PDF analysis result received.");
                    }
                }
                else
                {
                    // Handle the error response
                    throw new DecideException("Error getting PDF analysis result: " + response.ReasonPhrase);
                }
            }
        }
        catch (Exception ex)
        {
            throw new DecideException("Error getting PDF analysis result: " + ex.Message);
        }
    }

    // Scorecard Module
    public void CreateScorecard()
    {
        // Implement the logic to create a scorecard using the API
        // Include the access token in the request headers
        // Handle any exceptions that may occur
    }

    public void ReadScorecard()
    {
        // Implement the logic to read a scorecard using the API
        // Include the access token in the request headers
        // Handle any exceptions that may occur
    }

    public void DeleteScorecard()
    {
        // Implement the logic to delete a scorecard using the API
        // Include the access token in the request headers
        // Handle any exceptions that may occur
    }

    public void ExecuteScorecard()
    {
        // Implement the logic to execute a scorecard using the API
        // Include the access token in the request headers
        // Handle any exceptions that may occur
    }

}