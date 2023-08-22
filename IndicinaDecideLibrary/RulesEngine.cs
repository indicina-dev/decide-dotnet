# nullable disable
using Newtonsoft.Json;

namespace IndicinaDecideLibrary;

public static class Outcome
{
    public const string ACCEPT = "OUTCOME_ACCEPT";
    public const string DECLINE = "OUTCOME_DECLINE";
    public const string MANUAL_REVIEW = "OUTCOME_MANUAL_REVIEW";
}

public static class Condition
{
    public const string UNSPECIFIED = "CONDITION_UNSPECIFIED";
    public const string LESS_THAN = "CONDITION_LESS_THAN";
    public const string LESS_THAN_EQUAL_TO = "CONDITION_LESS_THAN_EQUAL_TO";
    public const string GREATER_THAN = "CONDITION_GREATER_THAN";
    public const string GREATER_THAN_EQUAL_TO = "CONDITION_GREATER_THAN_EQUAL_TO";
    public const string IS_BETWEEN = "CONDITION_IS_BETWEEN";
    public const string IS_NOT_BETWEEN = "CONDITION_IS_NOT_BETWEEN";
    public const string IS_EQUAL = "CONDITION_IS_EQUAL";
    public const string IS_NOT_EQUAL = "CONDITION_IS_NOT_EQUAL";
    public const string IS_IN = "CONDITION_IS_IN";
}

public static class Operator
{
    public const string NONE = "OPERATOR_NONE";
    public const string AND = "OPERATOR_AND";
    public const string OR = "OPERATOR_OR";
}

public static class Status
{
    public const string ENABLED = "enabled";
    public const string DISABLED = "disabled";
}

public static class RuleType
{
    public const string ACCOUNT_SWEEP = "behaviouralAnalysis.accountSweep";
    public const string GAMBLING_RATE = "behaviouralAnalysis.gamblingRate";
    public const string CONFIDENCE_INTERVAL_SALARY_DETECTION = "incomeAnalysis.confidenceIntervalonSalaryDetection";
    public const string LOAN_INFLOW_RATE = "behaviouralAnalysis.loanInflowRate";
    public const string LOAN_REPAYMENT_INFLOW_RATE = "behaviouralAnalysis.loanRepaymentInflowRate";
    public const string SALARY_EARNER = "incomeAnalysis.salaryEarner";
    public const string GIG_WORKER = "incomeAnalysis.gigWorker";
    public const string AVERAGE_BALANCE = "cashFlowAnalysis.averageBalance";
    public const string CLOSING_BALANCE = "cashFlowAnalysis.closingBalance";
    public const string AVERAGE_CREDITS = "cashFlowAnalysis.averageCredits";
    public const string AVERAGE_DEBITS = "cashFlowAnalysis.averageDebits";
    public const string AVERAGE_OTHER_INCOME = "incomeAnalysis.averageOtherIncome";
    public const string AVERAGE_SALARY = "incomeAnalysis.averageSalary";
    public const string MEDIAN_INCOME = "incomeAnalysis.medianIncome";
    public const string AVERAGE_RECURRING_EXPENSE = "spendAnalysis.averageRecurringExpense";
    public const string TOTAL_EXPENSE = "spendAnalysis.totalExpenses";
    public const string SAVING_AND_INVESTMENTS = "spendAnalysis.savingsAndInvestments";
    public const string LOAN_AMOUNT = "behaviouralAnalysis.loanAmount";
    public const string LOAN_REPAYMENTS = "behaviouralAnalysis.loanRepayments";
    public const string FIRST_DAY = "cashFlowAnalysis.firstDay";
    public const string LAST_DAY = "cashFlowAnalysis.lastDay";
    public const string INFLOW_OUTFLOW_RATE = "behaviouralAnalysis.inflowOutflowRate";
    public const string EXPECTED_SALARY_DAY = "incomeAnalysis.expectedSalaryDay";
    public const string LAST_SALARY_DATE = "incomeAnalysis.lastSalaryDate";
    public const string NO_OF_TRANSACTING_MONTHS = "cashFlowAnalysis.noOfTransactingMonths";
    public const string ACCOUNT_ACTIVITY = "cashFlowAnalysis.accountActivity";
    public const string NUMBER_SALARY_PAYMENTS = "incomeAnalysis.numberSalaryPayments";
    public const string NO_OF_OTHER_INCOME_PAYMENTS = "incomeAnalysis.numberOtherIncomePayments";
    public const string SALARY_FREQUENCY = "incomeAnalysis.salaryFrequency";
}

public class Rule
{
    public int order { get; set; }
    public string value { get; set; }
    public string type { get; set; }
    public string condition { get; set; }
    public string @operator { get; set; }
}

public class Block
{
    public List<Rule> rules { get; set; }
    public int order { get; set; }
    public string @operator { get; set; }
    public string negativeOutcome { get; set; }
}

public class BooleanRuleSet
{
    public string name { get; set; }
    public string positiveOutcome { get; set; }
    public string negativeOutcome { get; set; }
    public string owner { get; set; }
    public List<Block> blocks { get; set; }
}

public class MonthlyDuration
{
    public string duration { get; set; }
}

public class Affordability
{
    public int monthly_interest_rate { get; set; }
    public List<MonthlyDuration> monthly_duration { get; set; }
}

public class CreateScorecardRequest
{
    public string name { get; set; }
    public BooleanRuleSet booleanRuleSet { get; set; }
    public Affordability affordability { get; set; }
    public string status { get; set; }
}

public class CreateScorecardResponse
{
    public int statusCode { get; set; }
    public ScorecardData data { get; set; }
    public string message { get; set; }
}

public class ScorecardData
{
    public Scorecard scorecard { get; set; }
}

public class ReadScorecardResponse
{
    public int statusCode { get; set; }
    public Scorecard data { get; set; }
    public string message { get; set; }
}

public class Scorecard
{
    public int id { get; set; }
    public string name { get; set; }
    public Affordability affordability { get; set; }
    public BooleanRuleSet booleanRuleSet { get; set; }
    public string owner { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
}

public class ScorecardExecutionResult
{
    public int StatusCode { get; set; }
    public ScorecardExecutionData Data { get; set; }
    public string Message { get; set; }
}

public class ScorecardExecutionData
{
    public List<ScorecardResult> ScorecardResults { get; set; }
    public DecideResponse BankStatementSummary { get; set; }
}

public class ScorecardResult : JsonSerializable
{
    public string Name { get; set; }

    public string AnalysisId { get; set; }

    public long ScorecardId { get; set; }

    public string Status { get; set; }

    public string Message { get; set; }

    public AffordabilityModel Affordability { get; set; }

    public RulesModel Rules { get; set; }

    public class AffordabilityModel : JsonSerializable
    {
        public Breakdown[] Breakdown { get; set; }

        public string Currency { get; set; }
    }

    public class Breakdown : JsonSerializable
    {
        public long Tenor { get; set; }

        public string TenorType { get; set; }

        public double Value { get; set; }
    }

    public class RulesModel : JsonSerializable
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public RuleSet RuleSet { get; set; }

        public BlockOutcome Outcome { get; set; }

        public BlockElement[] Blocks { get; set; }
    }

    public class BlockElement : JsonSerializable
    {
        public RuleElement[] Rules { get; set; }

        public BlockBlock Block { get; set; }

        public BlockOutcome Outcome { get; set; }
    }

    public class BlockBlock : JsonSerializable
    {
        public long Order { get; set; }

        public string Operator { get; set; }

        public string NegativeOutcome { get; set; }
    }

    public class BlockOutcome : JsonSerializable
    {
        public bool Pass { get; set; }

        public string Action { get; set; }
    }

    public class RuleElement : JsonSerializable
    {
        public RuleRule Rule { get; set; }

        public Input Input { get; set; }

        public RuleOutcome Outcome { get; set; }
    }

    public class Input : JsonSerializable
    {
        public string Value { get; set; }

        public bool Skipped { get; set; }
    }

    public class RuleOutcome : JsonSerializable
    {
        public bool Pass { get; set; }
    }

    public class RuleRule : JsonSerializable
    {
        public long Order { get; set; }

        public string Value { get; set; }

        public string RuleType { get; set; }

        public string Condition { get; set; }

        public string Operator { get; set; }
    }

    public class RuleSet : JsonSerializable
    {
        public string NegativeOutcome { get; set; }

        public string PositiveOutcome { get; set; }
    }

    // public struct Value
    // {
    //     public DateTimeOffset? DateTime;
    //     public long? Integer;

    //     public static implicit operator Value(DateTimeOffset DateTime) => new Value { DateTime = DateTime };
    //     public static implicit operator Value(long Integer) => new Value { Integer = Integer };
    // }

}

public static class ScorecardResultExtensions
{
    public static string ToJson(this List<ScorecardResult> scorecardResults)
    {
        return JsonConvert.SerializeObject(scorecardResults);
    }
}

