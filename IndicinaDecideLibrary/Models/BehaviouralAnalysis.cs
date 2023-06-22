# nullable disable
using Newtonsoft.Json;

namespace IndicinaDecideLibrary
{
    public class BehaviouralAnalysis: JsonSerializable
    {
        [JsonProperty("accountSweep")]
        public string AccountSweep { get; set; }

        [JsonProperty("gamblingRate")]
        public long GamblingRate { get; set; }

        [JsonProperty("inflowOutflowRate")]
        public string InflowOutflowRate { get; set; }

        [JsonProperty("loanAmount")]
        public long LoanAmount { get; set; }

        [JsonProperty("loanInflowRate")]
        public long LoanInflowRate { get; set; }

        [JsonProperty("loanRepaymentInflowRate")]
        public long LoanRepaymentInflowRate { get; set; }

        [JsonProperty("loanRepayments")]
        public long LoanRepayments { get; set; }

        [JsonProperty("topIncomingTransferAccount")]
        public string TopIncomingTransferAccount { get; set; }

        [JsonProperty("topTransferRecipientAccount")]
        public string TopTransferRecipientAccount { get; set; }
    }
}
