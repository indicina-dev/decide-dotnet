# nullable disable

using System;
using System.Collections.Generic;

namespace IndicinaDecideLibrary
{
    public class ExpenseChannels: JsonSerializable
    {
        public object atmSpend { get; set; }
        public object webSpend { get; set; }
        public object posSpend { get; set; }
        public object ussdTransactions { get; set; }
        public object mobileSpend { get; set; }
        public object spendOnTransfers { get; set; }
        public object internationalTransactionsSpend { get; set; }
    }
}
