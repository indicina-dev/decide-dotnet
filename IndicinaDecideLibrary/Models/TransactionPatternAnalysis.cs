# nullable disable

using System;
using Newtonsoft.Json;

namespace IndicinaDecideLibrary
{
    public class TransactionPatternAnalysis: JsonSerializable
    {
        [JsonProperty("MAWWZeroBalanceInAccount")]
        public MAWWZeroBalanceInAccount MawwZeroBalanceInAccount { get; set; }

        [JsonProperty("NODWBalanceLess5000")]
        public long NodwBalanceLess5000 { get; set; }

        [JsonProperty("NODWBalanceLess")]
        public NODWBalanceLess NodwBalanceLess { get; set; }

        [JsonProperty("highestMAWOCredit")]
        public MAWWZeroBalanceInAccount HighestMawoCredit { get; set; }

        [JsonProperty("highestMAWODebit")]
        public MAWWZeroBalanceInAccount HighestMawoDebit { get; set; }

        [JsonProperty("lastDateOfCredit")]
        public object LastDateOfCredit { get; set; }

        [JsonProperty("lastDateOfDebit")]
        public DateTimeOffset LastDateOfDebit { get; set; }

        [JsonProperty("mostFrequentBalanceRange")]
        public string MostFrequentBalanceRange { get; set; }

        [JsonProperty("mostFrequentTransactionRange")]
        public string MostFrequentTransactionRange { get; set; }

        [JsonProperty("recurringExpense")]
        public RecurringExp[] RecurringExpense { get; set; }

        [JsonProperty("transactionsBetween100000And500000")]
        public long TransactionsBetween100000And500000 { get; set; }

        [JsonProperty("transactionsBetween10000And100000")]
        public long TransactionsBetween10000And100000 { get; set; }

        [JsonProperty("transactionsGreater500000")]
        public long TransactionsGreater500000 { get; set; }

        [JsonProperty("transactionsLess10000")]
        public long TransactionsLess10000 { get; set; }

        [JsonProperty("transactionRanges")]
        public TransactionRange[] TransactionRanges { get; set; }

        public class MAWWZeroBalanceInAccount: JsonSerializable
        {
            [JsonProperty("month")]
            public string Month { get; set; }

            [JsonProperty("week_of_month")]
            public long WeekOfMonth { get; set; }
        }

        public class NODWBalanceLess: JsonSerializable
        {
            [JsonProperty("amount")]
            public long Amount { get; set; }

            [JsonProperty("count")]
            public long Count { get; set; }
        }

        public class RecurringExp: JsonSerializable
        {
            [JsonProperty("amount")]
            public long Amount { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }

        public class TransactionRange: JsonSerializable
        {
            [JsonProperty("min")]
            public long? Min { get; set; }

            [JsonProperty("max")]
            public long? Max { get; set; }

            [JsonProperty("count")]
            public long Count { get; set; }
        }
        private static readonly Dictionary<string, Type> _analysis_call_dict = new Dictionary<string, Type>
        {
        };
    }
}
