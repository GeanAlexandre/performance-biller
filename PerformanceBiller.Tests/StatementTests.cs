using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerformanceBiller.Helpers;
using PerformanceBiller.Management;
using PerformanceBiller.Management.PlaysHandler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace PerformanceBiller.Tests
{
    public class StatementTests
    {
        const string expectedOutput = "Statement for BigCo\r\n" +
            " Hamlet: $650.00 (55 seats)\r\n" +
            " As You Like It: $580.00 (35 seats)\r\n" +
            " Othello: $500.00 (40 seats)\r\n" +
            "Amount owed is $1,730.00\r\n" +
            "You earned 47 credits\r\n";

        [Fact]
        public void Statement_can_run()
        {

            var statement = new Statement();

            using (var invoicesFile = File.OpenText("..\\..\\..\\invoices.json"))
            using (var invoicesReader = new JsonTextReader(invoicesFile))
            using (var playsFile = File.OpenText("..\\..\\..\\plays.json"))
            using (var playsReader = new JsonTextReader(playsFile))
            {
                var invoices = (JArray)JToken.ReadFrom(invoicesReader);

                var invoice = (JObject)invoices.First;

                var plays = (JObject)JToken.ReadFrom(playsReader);

                var actualResult = statement.Run(invoice, plays);

                Assert.Equal(expectedOutput, actualResult);
            }
        }


        [Fact]
        public void Should_Run_Statement()
        {
            const string baseJsonPath = @"..\\..\\..\";

            var plays = JsonFileReader
                .From(baseJsonPath)
                .Plays()
                .Deserialize<Dictionary<string, Play>>();

            var invoices = JsonFileReader
                .From(baseJsonPath)
                .Invoices()
                .Deserialize<Invoice[]>()
                .Select(invoice =>
                     new Invoice
                     {
                         Customer = invoice.Customer,
                         Performances = invoice
                        .Performances
                        .Select(perfomace => new Perfomance
                        {
                            PlayId = perfomace.PlayId,
                            Play = plays.GetValueOrDefault(perfomace.PlayId),
                            Audience = perfomace.Audience
                        })
                     });

            var summaryReport = Management.Statement
                .EnUs(invoices.FirstOrDefault())
                .Calculate(new PlayHandlerFactory())
                .Report(statement =>
                     Reports.For(statement).Summary()
                );

            Assert.Equal(expectedOutput, summaryReport);
        }
    }
}
