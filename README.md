# decide-dotnet Library - WIP ⚠️

The decide-dotnet Library is a C# library that provides functionality to analyze different types of bank statements, such as PDF, CSV, and JSON statements. It integrates with the Indicina Decide API to perform statement analysis and retrieve analysis results.

## Features

- Supports PDF, CSV, and JSON statement analysis.
- Retrieves authorization access token for API authentication.
- Sends API requests to initiate statement analysis.
- Monitors the status of PDF statement analysis.
- Parses analysis results into a standardized format.

## Requirements

- .NET Core 7.0 or later

## Installation

1. Clone the repository or download the source code.
2. Add the `decide-dotnet` library to your solution.

## Usage

1. Make sure you have the required client ID and client secret set as environment variables.
```bash
export INDICINA_CLIENT_ID=your_indicina_client_id
export INDICINA_CLIENT_SECRET=your_indicina_client_secret
```
2. Create a new DecideAPI instance.
3. Call the necessary method as related to your statement type.
    - AnalyzeJsonStatement
    - AnalyzeCSVStatement
    - InitiatePdfAnalysis
4. Retrieve and process the analysis results returned by the library.

Here's an example of using the library to analyze a PDF statement:

N:B PDF Analysis are not provided instantly, you get a job_id when you initiate the analysis
You can then use this job_id to monitor the status of the pdf analysis Job

```csharp
using IndicinaDecideLibrary;

Customer customer = new Customer(customer_id: "12345", email: "example@email.com",
    first_name: "John", last_name: "Doe", phone: "1234567890");

DecideAPI api = new();

string jobID = api.InitiatePdfAnalysis(pdfPath: "test.pdf", currency: Currency.NGN, 
                bank: Bank.STERLING, customer: customer);

PdfAnalysisResult resData;
do
{
    // Get the PDF analysis result
    resData = api.GetPdfAnalysisResult(jobID);

    // Print the status
    Console.WriteLine(resData.Data.Status);

    // Sleep for a certain duration before checking again
    Thread.Sleep(7000); // Sleep for 7 second (adjust the duration as needed)

} while (resData.Data.Status != PDFStatus.DONE.ToString() && resData.Data.Status != PDFStatus.FAILED.ToString());

// You can use dot referencing to access the values in the response
// We have used a convenient ToJson() to help you see the results
Console.WriteLine(resData.Data.Status);
Console.WriteLine(resData.Data.DecideResponse.CashFlowAnalysis.ToJson());
Console.WriteLine(resData.Data.DecideResponse.BehaviouralAnalysis.ToJson());
Console.WriteLine(resData.Data.DecideResponse.SpendAnalysis.ToJson());
Console.WriteLine(resData.Data.DecideResponse.IncomeAnalysis.ToJson());
```

And here's an example of using the library to analyze a CSV statement:

```csharp
using IndicinaDecideLibrary;

Customer customer = new Customer(customer_id: "12345", email: "example@email.com",
    first_name: "John", last_name: "Doe", phone: "1234567890");

// Define the csv path
string csvPath = "example.csv"

DecideAPI api = new();

DefaultAnalysisResult response = api.AnalyzeCsvStatement(csvPath: csvPath, customer: customer);

// You can use dot referencing to access the values in the response
// We have used a convenient ToJson() to help you see the results
Console.WriteLine(response.Status);
Console.WriteLine(response.Data.CashFlowAnalysis.ToJson());
Console.WriteLine(response.Data.BehaviouralAnalysis.ToJson());
Console.WriteLine(response.Data.SpendAnalysis.ToJson());
Console.WriteLine(response.Data.IncomeAnalysis.ToJson());
```

And here's an example of using the library to analyze a JSON statement:
```csharp
using IndicinaDecideLibrary;

Customer customer = new Customer(customer_id: "12345", email: "example@email.com",
    first_name: "John", last_name: "Doe", phone: "1234567890");

// Define the statement format and string
StatementFormat statementFormat = StatementFormat.MONO;

string statementString = @"{
    ""paging"": {
        ""total"": 190,
        ""page"": 2,
        ""previous"": ""https://api.withmono.com/accounts/:id/transactions?page=2"",
        ""next"": ""https://api.withmono.com/accounts/:id/transactions?page=3""
    },
    ""data"": [
        {
            ""_id"": ""5f171a54xxxxxxxxxxxx1154"",
            ""amount"": 10000,
            ""date"": ""2020-07-21T00:00:00.000Z"",
            ""narration"": ""TRANSFER from JOHN DOE to JANE SMITH"",
            ""type"": ""debit"",
            ""category"": ""E-CHANNELS""
        },
        {
            ""_id"": ""5d171a54xxxxxxxxxxxx6654"",
            ""amount"": 20000,
            ""date"": ""2020-07-21T00:00:00.000Z"",
            ""narration"": ""TRANSFER from JOHN DOE to EVA SMITH"",
            ""type"": ""debit"",
            ""category"": ""E-CHANNELS""
        }
    ]
}";

DecideAPI api = new();

DefaultAnalysisResult response = api.AnalyzeJsonStatement(statementFormat, statementString, customer);

// You can use dot referencing to access the values in the response
// We have used a convenient ToJson() to help you see the results
Console.WriteLine(response.Status);
Console.WriteLine(response.Data.CashFlowAnalysis.ToJson());
Console.WriteLine(response.Data.BehaviouralAnalysis.ToJson());
Console.WriteLine(response.Data.SpendAnalysis.ToJson());
Console.WriteLine(response.Data.IncomeAnalysis.ToJson());
```

### Running Analysis with ScoreCard
You can run a statement analysis with scorecard ids you have created as described below.
```csharp
using IndicinaDecideLibrary;

Customer customer = new Customer(customer_id: "12345", email: "example@email.com",
    first_name: "John", last_name: "Doe", phone: "1234567890");

// Define a list of precreated scorecard ids
List<int> scorecardIds = new List<int> { 102, 103, 104 };

// Define the statement format and string
StatementFormat statementFormat = StatementFormat.MONO;

string statementString = @"{
    ""paging"": {
        ""total"": 190,
        ""page"": 2,
        ""previous"": ""https://api.withmono.com/accounts/:id/transactions?page=2"",
        ""next"": ""https://api.withmono.com/accounts/:id/transactions?page=3""
    },
    ""data"": [
        {
            ""_id"": ""5f171a54xxxxxxxxxxxx1154"",
            ""amount"": 10000,
            ""date"": ""2020-07-21T00:00:00.000Z"",
            ""narration"": ""TRANSFER from JOHN DOE to JANE SMITH"",
            ""type"": ""debit"",
            ""category"": ""E-CHANNELS""
        },
        {
            ""_id"": ""5d171a54xxxxxxxxxxxx6654"",
            ""amount"": 20000,
            ""date"": ""2020-07-21T00:00:00.000Z"",
            ""narration"": ""TRANSFER from JOHN DOE to EVA SMITH"",
            ""type"": ""debit"",
            ""category"": ""E-CHANNELS""
        }
    ]
}";

DecideAPI api = new();

// Make sure to pass the list of scorecard ids
DefaultAnalysisResult response = api.AnalyzeJsonStatement(statementFormat, statementString, customer, scorecardIds);

// You can use dot referencing to access the values in the response
// We have used a convenient ToJson() to help you see the results
Console.WriteLine(response.Status);
Console.WriteLine(response.Data.CashFlowAnalysis.ToJson());
Console.WriteLine(response.Data.BehaviouralAnalysis.ToJson());
Console.WriteLine(response.Data.SpendAnalysis.ToJson());
Console.WriteLine(response.Data.IncomeAnalysis.ToJson());
Console.WriteLine(response.Data.ScorecardResult.ToJson());
```

Make sure to replace Bank.Access, customer, Currency.NGN, filePath, password, csvPath, format, and statementString with the appropriate values for your use case.

## Contributing
Contributions to the decide-dotnet Library are welcome! If you encounter any issues or have suggestions for improvements, please open an issue or submit a pull request on the project's GitHub repository.

## License
This project is licensed under the MIT License.
