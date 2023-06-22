# nullable disable

using System;
using System.Collections.Generic;

namespace IndicinaDecideLibrary
{
    public class ExpenseCategories: JsonSerializable
    {
        public object bills { get; set; }
        public object entertainment { get; set; }
        public object savingsAndInvestments { get; set; }
        public object gambling { get; set; }
        public object airtime { get; set; }
        public object bankCharges { get; set; }

    }
}
