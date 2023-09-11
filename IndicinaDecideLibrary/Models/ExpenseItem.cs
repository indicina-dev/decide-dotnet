# nullable disable

using System;
using System.Collections.Generic;

namespace IndicinaDecideLibrary
{
    public class ExpenseItem : JsonSerializable
    {
        public string Key { get; set; }
        public double Value { get; set; }
    }
}
