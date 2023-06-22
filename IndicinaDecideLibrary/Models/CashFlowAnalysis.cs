# nullable disable

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndicinaDecideLibrary
{
    public class CashFlowAnalysis: JsonSerializable
    {
        [JsonProperty("accountActivity")]
        public long AccountActivity { get; set; }

        [JsonProperty("averageBalance")]
        public long AverageBalance { get; set; }

        [JsonProperty("averageCredits")]
        public long AverageCredits { get; set; }

        [JsonProperty("averageDebits")]
        public long AverageDebits { get; set; }

        [JsonProperty("closingBalance")]
        public long ClosingBalance { get; set; }

        [JsonProperty("firstDay")]
        public DateTimeOffset FirstDay { get; set; }

        [JsonProperty("lastDay")]
        public DateTimeOffset LastDay { get; set; }

        [JsonProperty("monthPeriod")]
        public string MonthPeriod { get; set; }

        [JsonProperty("netAverageMonthlyEarnings")]
        public long NetAverageMonthlyEarnings { get; set; }

        [JsonProperty("noOfTransactingMonths")]
        public long NoOfTransactingMonths { get; set; }

        [JsonProperty("totalCreditTurnover")]
        public long TotalCreditTurnover { get; set; }

        [JsonProperty("totalDebitTurnover")]
        public long TotalDebitTurnover { get; set; }

        [JsonProperty("yearInStatement")]
        public string YearInStatement { get; set; }

        [JsonProperty("maxEmiEligibility")]
        public long MaxEmiEligibility { get; set; }

        [JsonProperty("emiConfidenceScore")]
        public double EmiConfidenceScore { get; set; }

        [JsonProperty("totalMonthlyCredit")]
        public MonthlyData[] TotalMonthlyCredit { get; set; }

        [JsonProperty("totalMonthlyDebit")]
        public MonthlyData[] TotalMonthlyDebit { get; set; }

        [JsonProperty("averageMonthlyCredit")]
        public MonthlyData[] AverageMonthlyCredit { get; set; }

        [JsonProperty("averageMonthlyDebit")]
        public MonthlyData[] AverageMonthlyDebit { get; set; }

        public partial class MonthlyData
        {
            [JsonProperty("month")]
            public string Month { get; set; }

            [JsonProperty("amount")]
            public long Amount { get; set; }
        }

    }
}
