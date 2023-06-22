# nullable disable

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndicinaDecideLibrary
{
    public class ScorecardResult: JsonSerializable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("analysisId")]
        public string AnalysisId { get; set; }

        [JsonProperty("scorecardId")]
        public long ScorecardId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("affordability")]
        public AffordabilityModel Affordability { get; set; }

        [JsonProperty("rules")]
        public RulesModel Rules { get; set; }

        public class AffordabilityModel: JsonSerializable
        {
            [JsonProperty("breakdown")]
            public Breakdown[] Breakdown { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }
        }

        public class Breakdown: JsonSerializable
        {
            [JsonProperty("tenor")]
            public long Tenor { get; set; }

            [JsonProperty("tenor_type")]
            public string TenorType { get; set; }

            [JsonProperty("value")]
            public double Value { get; set; }
        }

        public class RulesModel: JsonSerializable
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("ruleSet")]
            public RuleSet RuleSet { get; set; }

            [JsonProperty("outcome")]
            public BlockOutcome Outcome { get; set; }

            [JsonProperty("blocks")]
            public BlockElement[] Blocks { get; set; }
        }

        public class BlockElement: JsonSerializable
        {
            [JsonProperty("rules")]
            public RuleElement[] Rules { get; set; }

            [JsonProperty("block")]
            public BlockBlock Block { get; set; }

            [JsonProperty("outcome")]
            public BlockOutcome Outcome { get; set; }
        }

        public class BlockBlock: JsonSerializable
        {
            [JsonProperty("order")]
            public long Order { get; set; }

            [JsonProperty("operator")]
            public string Operator { get; set; }

            [JsonProperty("negativeOutcome")]
            public string NegativeOutcome { get; set; }
        }

        public class BlockOutcome: JsonSerializable
        {
            [JsonProperty("pass")]
            public bool Pass { get; set; }

            [JsonProperty("action")]
            public string Action { get; set; }
        }

        public class RuleElement: JsonSerializable
        {
            [JsonProperty("rule")]
            public RuleRule Rule { get; set; }

            [JsonProperty("input")]
            public Input Input { get; set; }

            [JsonProperty("outcome")]
            public RuleOutcome Outcome { get; set; }
        }

        public class Input: JsonSerializable
        {
            [JsonProperty("value")]
            public Value Value { get; set; }

            [JsonProperty("skipped")]
            public bool Skipped { get; set; }
        }

        public class RuleOutcome: JsonSerializable
        {
            [JsonProperty("pass")]
            public bool Pass { get; set; }
        }

        public class RuleRule: JsonSerializable
        {
            [JsonProperty("order")]
            public long Order { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("ruleType")]
            public string RuleType { get; set; }

            [JsonProperty("condition")]
            public string Condition { get; set; }

            [JsonProperty("operator")]
            public string Operator { get; set; }
        }

        public class RuleSet: JsonSerializable
        {
            [JsonProperty("negativeOutcome")]
            public string NegativeOutcome { get; set; }

            [JsonProperty("positiveOutcome")]
            public string PositiveOutcome { get; set; }
        }

        public struct Value
        {
            public DateTimeOffset? DateTime;
            public long? Integer;

            public static implicit operator Value(DateTimeOffset DateTime) => new Value { DateTime = DateTime };
            public static implicit operator Value(long Integer) => new Value { Integer = Integer };
        }
    }
}