using System.Reflection;
using IndicinaDecideLibrary;


namespace IndicinaDecideLibraryTests
{
    [TestClass]
    public class DecideAPITests
    {
        [TestMethod]
        public void AnalyzeJson_Successful()
        {
            // Arrange
            var successfulResponse = "{\"status\":\"success\",\"data\":{\"country\":\"Nigeria\",\"currency\":\"NGN\",\"behaviouralAnalysis\":{\"accountSweep\":\"Yes\",\"gamblingRate\":0,\"inflowOutflowRate\":\"Negative Cash Flow\",\"loanAmount\":0,\"loanInflowRate\":0,\"loanRepaymentInflowRate\":0,\"loanRepayments\":0,\"topIncomingTransferAccount\":\"no top incoming transfer account found\",\"topTransferRecipientAccount\":\"no top recipient account found\"},\"cashFlowAnalysis\":{\"accountActivity\":0,\"averageBalance\":0,\"averageCredits\":0,\"averageDebits\":150,\"closingBalance\":0,\"firstDay\":\"2020-07-21\",\"lastDay\":\"2020-07-21\",\"monthPeriod\":\"July - July\",\"netAverageMonthlyEarnings\":-300,\"noOfTransactingMonths\":1,\"totalCreditTurnover\":0,\"totalDebitTurnover\":300,\"yearInStatement\":\"2020\",\"maxEmiEligibility\":0,\"emiConfidenceScore\":0.8,\"totalMonthlyCredit\":null,\"totalMonthlyDebit\":[{\"month\":\"2020-07\",\"amount\":300}],\"averageMonthlyCredit\":null,\"averageMonthlyDebit\":[{\"month\":\"2020-07\",\"amount\":150}]},\"incomeAnalysis\":{\"salaryEarner\":\"No\",\"medianIncome\":0,\"averageOtherIncome\":0,\"expectedSalaryDay\":0,\"lastSalaryDate\":null,\"averageSalary\":0,\"confidenceIntervalonSalaryDetection\":0,\"salaryFrequency\":null,\"numberSalaryPayments\":0,\"numberOtherIncomePayments\":0,\"gigWorker\":\"No\"},\"spendAnalysis\":{\"averageRecurringExpense\":0,\"hasRecurringExpense\":\"No\",\"averageMonthlyExpenses\":300,\"expenseChannels\":[{\"key\":\"atmSpend\",\"value\":0},{\"key\":\"webSpend\",\"value\":0},{\"key\":\"posSpend\",\"value\":0},{\"key\":\"ussdTransactions\",\"value\":0},{\"key\":\"mobileSpend\",\"value\":0},{\"key\":\"spendOnTransfers\",\"value\":300},{\"key\":\"internationalTransactionsSpend\",\"value\":0}],\"expenseCategories\":[{\"key\":\"bills\",\"value\":0},{\"key\":\"entertainment\",\"value\":0},{\"key\":\"savingsAndInvestments\",\"value\":0},{\"key\":\"gambling\",\"value\":0},{\"key\":\"airtime\",\"value\":0},{\"key\":\"bankCharges\",\"value\":0},{\"key\":\"chequeWithdrawal\",\"value\":0},{\"key\":\"cashWithdrawal\",\"value\":0},{\"key\":\"shopping\",\"value\":0},{\"key\":\"eatingOut\",\"value\":0}]},\"transactionPatternAnalysis\":{\"MAWWZeroBalanceInAccount\":{\"month\":\"July\",\"week_of_month\":30},\"NODWBalanceLess5000\":1,\"NODWBalanceLess\":{\"amount\":5000,\"count\":1},\"highestMAWOCredit\":{\"month\":\"\",\"week_of_month\":0},\"highestMAWODebit\":{\"month\":\"July\",\"week_of_month\":4},\"lastDateOfCredit\":null,\"lastDateOfDebit\":\"2020-07-21\",\"mostFrequentBalanceRange\":\"<10000\",\"mostFrequentTransactionRange\":\"<10000\",\"recurringExpense\":[{\"amount\":0,\"description\":\"null\"}],\"transactionsBetween100000And500000\":0,\"transactionsBetween10000And100000\":0,\"transactionsGreater500000\":0,\"transactionsLess10000\":2,\"transactionRanges\":[{\"min\":10000,\"max\":100000,\"count\":0},{\"min\":100000,\"max\":500000,\"count\":0},{\"min\":null,\"max\":10000,\"count\":2},{\"min\":500000,\"max\":null,\"count\":0}]},\"scorecardResults\":[{\"name\":\"Dee_Test\",\"analysisId\":\"22b686e9456b2e74a2e7dbde29484eec182e85b206ed4f9507b882580f968cd6265c88fdbadc61dcf49b6f50da036a9d2759c574e8ec2deed99acdf24a04a0bb\",\"scorecardId\":123,\"status\":\"success\",\"message\":\"Successfully Executed Scorecard\",\"affordability\":{\"breakdown\":[{\"tenor\":1,\"tenor_type\":\"months\",\"value\":0},{\"tenor\":3,\"tenor_type\":\"months\",\"value\":0},{\"tenor\":6,\"tenor_type\":\"months\",\"value\":0}],\"currency\":\"NGN\"},\"rules\":{\"id\":\"64a81148375ccc0013a8fced\",\"name\":\"dee_test\",\"ruleSet\":{\"negativeOutcome\":\"Decline\",\"positiveOutcome\":\"Accept\"},\"outcome\":{\"pass\":false,\"action\":\"OUTCOME_DECLINE\"},\"blocks\":[{\"rules\":[{\"rule\":{\"order\":1,\"value\":\"0.40\",\"ruleType\":\"cashFlowAnalysis.accountActivity\",\"condition\":\"CONDITION_GREATER_THAN\",\"operator\":\"OPERATOR_NONE\"},\"input\":{\"value\":\"0\",\"skipped\":false},\"outcome\":{\"pass\":false}}],\"block\":{\"order\":1,\"operator\":\"OPERATOR_NONE\",\"negativeOutcome\":\"OUTCOME_MANUAL_REVIEW\"},\"outcome\":{\"pass\":false,\"action\":\"OUTCOME_DECLINE\"}}]}}],\"id\":\"22b686e9456b2e74a2e7dbde29484eec182e85b206ed4f9507b882580f968cd6265c88fdbadc61dcf49b6f50da036a9d2759c574e8ec2deed99acdf24a04a0bb\"}}";

            var authorization = new DecideAuth("clientId", "clientSecret", new MockHttpClientService());
            var decideApi = new DecideAPI(authorization, new MockHttpClientService(responseContent: successfulResponse));

            // Act
            Customer customer = new(customer_id: "12345", email: "example@email.com", first_name: "John", last_name: "Doe", phone: "1234567890");
            var result = decideApi.AnalyzeJson(StatementFormat.MONO, "{}", customer, null);

            // Assert
            Assert.IsNotNull(result); // Assuming DefaultAnalysisResult has proper structure to validate
        }

        [TestMethod]
        public void AnalyzeJson_InvalidJson_ThrowsException()
        {
            // Arrange
            var authorization = new DecideAuth("clientId", "clientSecret", new MockHttpClientService());
            var decideApi = new DecideAPI(authorization, new MockHttpClientService());

            // Act & Assert
            Customer customer = new(customer_id: "12345", email: "example@email.com", first_name: "John", last_name: "Doe", phone: "1234567890");
            Assert.ThrowsException<DecideException>(() => decideApi.AnalyzeJson(StatementFormat.MONO, "{invalid_json}", customer, null));
        }

        // [TestMethod]
        // public void AnalyzeCsv_Successful()
        // {
        //     // Arrange
        //     var successfulResponse = "{\"status\":\"success\",\"data\":{\"country\":\"Nigeria\",\"currency\":\"NGN\",\"behaviouralAnalysis\":{\"accountSweep\":\"Yes\",\"gamblingRate\":0,\"inflowOutflowRate\":\"Negative Cash Flow\",\"loanAmount\":0,\"loanInflowRate\":0,\"loanRepaymentInflowRate\":0,\"loanRepayments\":0,\"topIncomingTransferAccount\":\"no top incoming transfer account found\",\"topTransferRecipientAccount\":\"no top recipient account found\"},\"cashFlowAnalysis\":{\"accountActivity\":0,\"averageBalance\":0,\"averageCredits\":0,\"averageDebits\":150,\"closingBalance\":0,\"firstDay\":\"2020-07-21\",\"lastDay\":\"2020-07-21\",\"monthPeriod\":\"July - July\",\"netAverageMonthlyEarnings\":-300,\"noOfTransactingMonths\":1,\"totalCreditTurnover\":0,\"totalDebitTurnover\":300,\"yearInStatement\":\"2020\",\"maxEmiEligibility\":0,\"emiConfidenceScore\":0.8,\"totalMonthlyCredit\":null,\"totalMonthlyDebit\":[{\"month\":\"2020-07\",\"amount\":300}],\"averageMonthlyCredit\":null,\"averageMonthlyDebit\":[{\"month\":\"2020-07\",\"amount\":150}]},\"incomeAnalysis\":{\"salaryEarner\":\"No\",\"medianIncome\":0,\"averageOtherIncome\":0,\"expectedSalaryDay\":0,\"lastSalaryDate\":null,\"averageSalary\":0,\"confidenceIntervalonSalaryDetection\":0,\"salaryFrequency\":null,\"numberSalaryPayments\":0,\"numberOtherIncomePayments\":0,\"gigWorker\":\"No\"},\"spendAnalysis\":{\"averageRecurringExpense\":0,\"hasRecurringExpense\":\"No\",\"averageMonthlyExpenses\":300,\"expenseChannels\":[{\"key\":\"atmSpend\",\"value\":0},{\"key\":\"webSpend\",\"value\":0},{\"key\":\"posSpend\",\"value\":0},{\"key\":\"ussdTransactions\",\"value\":0},{\"key\":\"mobileSpend\",\"value\":0},{\"key\":\"spendOnTransfers\",\"value\":300},{\"key\":\"internationalTransactionsSpend\",\"value\":0}],\"expenseCategories\":[{\"key\":\"bills\",\"value\":0},{\"key\":\"entertainment\",\"value\":0},{\"key\":\"savingsAndInvestments\",\"value\":0},{\"key\":\"gambling\",\"value\":0},{\"key\":\"airtime\",\"value\":0},{\"key\":\"bankCharges\",\"value\":0},{\"key\":\"chequeWithdrawal\",\"value\":0},{\"key\":\"cashWithdrawal\",\"value\":0},{\"key\":\"shopping\",\"value\":0},{\"key\":\"eatingOut\",\"value\":0}]},\"transactionPatternAnalysis\":{\"MAWWZeroBalanceInAccount\":{\"month\":\"July\",\"week_of_month\":30},\"NODWBalanceLess5000\":1,\"NODWBalanceLess\":{\"amount\":5000,\"count\":1},\"highestMAWOCredit\":{\"month\":\"\",\"week_of_month\":0},\"highestMAWODebit\":{\"month\":\"July\",\"week_of_month\":4},\"lastDateOfCredit\":null,\"lastDateOfDebit\":\"2020-07-21\",\"mostFrequentBalanceRange\":\"<10000\",\"mostFrequentTransactionRange\":\"<10000\",\"recurringExpense\":[{\"amount\":0,\"description\":\"null\"}],\"transactionsBetween100000And500000\":0,\"transactionsBetween10000And100000\":0,\"transactionsGreater500000\":0,\"transactionsLess10000\":2,\"transactionRanges\":[{\"min\":10000,\"max\":100000,\"count\":0},{\"min\":100000,\"max\":500000,\"count\":0},{\"min\":null,\"max\":10000,\"count\":2},{\"min\":500000,\"max\":null,\"count\":0}]},\"scorecardResults\":[{\"name\":\"Dee_Test\",\"analysisId\":\"22b686e9456b2e74a2e7dbde29484eec182e85b206ed4f9507b882580f968cd6265c88fdbadc61dcf49b6f50da036a9d2759c574e8ec2deed99acdf24a04a0bb\",\"scorecardId\":123,\"status\":\"success\",\"message\":\"Successfully Executed Scorecard\",\"affordability\":{\"breakdown\":[{\"tenor\":1,\"tenor_type\":\"months\",\"value\":0},{\"tenor\":3,\"tenor_type\":\"months\",\"value\":0},{\"tenor\":6,\"tenor_type\":\"months\",\"value\":0}],\"currency\":\"NGN\"},\"rules\":{\"id\":\"64a81148375ccc0013a8fced\",\"name\":\"dee_test\",\"ruleSet\":{\"negativeOutcome\":\"Decline\",\"positiveOutcome\":\"Accept\"},\"outcome\":{\"pass\":false,\"action\":\"OUTCOME_DECLINE\"},\"blocks\":[{\"rules\":[{\"rule\":{\"order\":1,\"value\":\"0.40\",\"ruleType\":\"cashFlowAnalysis.accountActivity\",\"condition\":\"CONDITION_GREATER_THAN\",\"operator\":\"OPERATOR_NONE\"},\"input\":{\"value\":\"0\",\"skipped\":false},\"outcome\":{\"pass\":false}}],\"block\":{\"order\":1,\"operator\":\"OPERATOR_NONE\",\"negativeOutcome\":\"OUTCOME_MANUAL_REVIEW\"},\"outcome\":{\"pass\":false,\"action\":\"OUTCOME_DECLINE\"}}]}}],\"id\":\"22b686e9456b2e74a2e7dbde29484eec182e85b206ed4f9507b882580f968cd6265c88fdbadc61dcf49b6f50da036a9d2759c574e8ec2deed99acdf24a04a0bb\"}}";
        //     var authorization = new Authorization("clientId", "clientSecret", new MockHttpClientService());
        //     var decideApi = new DecideAPI(authorization, new MockHttpClientService(responseContent: successfulResponse));

        //     // Act
        //     Customer customer = new(customer_id: "12345", email: "example@email.com", first_name: "John", last_name: "Doe", phone: "1234567890");
        //     var result = decideApi.AnalyzeCsv("path/to/csv", customer, null);

        //     // Assert
        //     Assert.IsNotNull(result); // Assuming DefaultAnalysisResult has proper structure to validate
        // }

        [TestMethod]
        public void InitiatePdfAnalysis_Successful()
        {
            // Arrange
            string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.pdf");
            string resourceName = "IndicinaDecideTest.TestFiles.sample.pdf";
            // Copy embedded resource to the file path
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (var fileStream = new FileStream(pdfPath, FileMode.Create))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("The source stream is null.");
                }

                if (fileStream == null)
                {
                    throw new InvalidOperationException("The destination file stream is null.");
                }

                stream.CopyTo(fileStream);

            }

            var authorization = new DecideAuth("clientId", "clientSecret", new MockHttpClientService());
            var decideApi = new DecideAPI(authorization, new MockHttpClientService());
            Customer customer = new(customer_id: "12345", email: "example@email.com", first_name: "John", last_name: "Doe", phone: "1234567890");

            // Act
            var result = decideApi.InitiatePdfAnalysis(pdfPath, Currency.NGN, Bank.GTB, customer);

            // Assert
            Assert.IsNotNull(result); // Validate the result as needed

            // Cleanup
            File.Delete(pdfPath);
        }

        [TestMethod]
        public void CreateScorecard_Successful()
        {
            // Arrange
            var authorization = new DecideAuth("clientId", "clientSecret", new MockHttpClientService());
            var successfulResponse = "{\"statusCode\":200,\"data\":{\"scorecard\":{\"name\":\"Sample Scorecard\",\"booleanRulesetId\":\"64e60ddf6228b90013457742\",\"affordability\":{\"monthly_interest_rate\":5,\"monthly_duration\":[{\"duration\":\"12\"}]},\"owner\":\"indicina\",\"status\":\"enabled\",\"createdAt\":\"2023-08-23T13:47:12.047Z\",\"updatedAt\":\"2023-08-23T13:47:12.047Z\",\"id\":143}},\"message\":\"Score card created successfully\"}"
;
            var decideApi = new DecideAPI(authorization, new MockHttpClientService(responseContent: successfulResponse));
            var request = new CreateScorecardRequest
            {
                name = "Sample Scorecard",
                status = Status.ENABLED,
                affordability = new Affordability
                {
                    monthly_interest_rate = 5,
                    monthly_duration = new List<MonthlyDuration> { new MonthlyDuration { duration = "12" } }
                },
                booleanRuleSet = new BooleanRuleSet
                {
                    name = "Sample Rule Set",
                    positiveOutcome = Outcome.ACCEPT,
                    negativeOutcome = Outcome.DECLINE,
                    owner = "user_id",
                    blocks = new List<Block>
                    {
                        new Block
                        {
                            order = 1,
                            @operator = Operator.AND,
                            negativeOutcome = Outcome.DECLINE,
                            rules = new List<Rule>
                            {
                                new Rule
                                {
                                    order = 1,
                                    value = "5000",
                                    type = RuleType.AVERAGE_BALANCE,
                                    condition = Condition.GREATER_THAN_EQUAL_TO,
                                    @operator = Operator.NONE
                                }
                            }
                        }
                    }
                }
            };

            // Act
            var scorecardId = decideApi.CreateScorecard(request);

            // Assert
            Assert.IsNotNull(scorecardId);
        }

        [TestMethod]
        public void DeleteScorecard_Successful()
        {
            // Arrange
            var authorization = new DecideAuth("clientId", "clientSecret", new MockHttpClientService());
            var decideApi = new DecideAPI(authorization, new MockHttpClientService());

            // Act
            var result = decideApi.DeleteScorecard("scorecardId");

            // Assert
            Assert.IsNotNull(result); // Validate response as needed
        }

        [TestMethod]
        public void ExecuteScorecard_Successful()
        {
            // Arrange
            var authorization = new DecideAuth("clientId", "clientSecret", new MockHttpClientService());
            var decideApi = new DecideAPI(authorization, new MockHttpClientService());

            // Act
            var result = decideApi.ExecuteScorecard("analysisId", new List<int> { 1, 2, 3 });

            // Assert
            Assert.IsNotNull(result); // Validate ScorecardExecutionResult as needed
        }
    }
}
