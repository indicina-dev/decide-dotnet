# nullable disable
namespace IndicinaDecideLibrary;

public class InitialPdfAnalysisResponse
{
    public string Status { get; set; }
    public InitialPdfAnalysisData Data { get; set; }
}

public class InitialPdfAnalysisData
{
    public string JobId { get; set; }
    public string Status { get; set; }
}

public class PdfAnalysisResult
{
    public string Status { get; set; }
    public PdfAnalysisResultData Data { get; set; }
}

public class PdfAnalysisResultData
{
    public string JobId { get; set; }
    public List<string> ScoreCardIds { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public DecideResponse DecideResponse { get; set; }
}

public class DecideResponse : JsonSerializable
{
    public string Country { get; set; }
    public string Currency { get; set; }
    public BehaviouralAnalysis BehaviouralAnalysis { get; set; }
    public CashFlowAnalysis CashFlowAnalysis { get; set; }
    public IncomeAnalysis IncomeAnalysis { get; set; }
    public SpendAnalysis SpendAnalysis { get; set; }
    public List<ScorecardResult> ScorecardResults { get; set; }
    public required string Id { get; set; }
}

public class DefaultAnalysisResult : JsonSerializable
{
    public string Status { get; set; }
    public DecideResponse Data { get; set; }
}

public class BankStatement
{
    public string Type { get; set; }
    public string Content { get; set; }
}
