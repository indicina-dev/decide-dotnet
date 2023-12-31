# nullable disable

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndicinaDecideLibrary
{
    public class SpendAnalysis : JsonSerializable
    {
        [JsonProperty("averageRecurringExpense")]
        public long AverageRecurringExpense { get; set; }

        [JsonProperty("hasRecurringExpense")]
        public string HasRecurringExpense { get; set; }

        [JsonProperty("averageMonthlyExpenses")]
        public long AverageMonthlyExpenses { get; set; }

        [JsonProperty("expenseChannels")]
        public List<ExpenseItem> ExpenseChannels { get; set; }

        [JsonProperty("expenseCategories")]
        public List<ExpenseItem> ExpenseCategories { get; set; }
    }
}
