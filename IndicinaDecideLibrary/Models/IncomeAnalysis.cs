# nullable disable

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndicinaDecideLibrary
{
    public class IncomeAnalysis: JsonSerializable
    {
        [JsonProperty("salaryEarner")]
        public string SalaryEarner { get; set; }

        [JsonProperty("medianIncome")]
        public long MedianIncome { get; set; }

        [JsonProperty("averageOtherIncome")]
        public long AverageOtherIncome { get; set; }

        [JsonProperty("expectedSalaryDay")]
        public long ExpectedSalaryDay { get; set; }

        [JsonProperty("lastSalaryDate")]
        public object LastSalaryDate { get; set; }

        [JsonProperty("averageSalary")]
        public long AverageSalary { get; set; }

        [JsonProperty("confidenceIntervalonSalaryDetection")]
        public long ConfidenceIntervalonSalaryDetection { get; set; }

        [JsonProperty("salaryFrequency")]
        public object SalaryFrequency { get; set; }

        [JsonProperty("numberSalaryPayments")]
        public long NumberSalaryPayments { get; set; }

        [JsonProperty("numberOtherIncomePayments")]
        public long NumberOtherIncomePayments { get; set; }

        [JsonProperty("gigWorker")]
        public string GigWorker { get; set; }
    }
}
