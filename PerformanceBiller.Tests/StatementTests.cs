using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerformanceBiller.Statement.Models;
using PerformanceBiller.Statement.Reports;
using PerformanceBiller.Statement.Storage;
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

        const string expectedOutputPt = "Statement for BigCo\r\n" +
            " Hamlet: R$ 650,00 (55 seats)\r\n" +
            " As You Like It: R$ 580,00 (35 seats)\r\n" +
            " Othello: R$ 500,00 (40 seats)\r\n" +
            "Amount owed is R$ 1.730,00\r\n" +
            "You earned 47 credits\r\n";

        [Fact]
        public void Statement_can_run()
        {

            var statement = new StatementOld();

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
            var invoiceStorage = new InvoiceStorage();
            var invoice = invoiceStorage.GetAll().FirstOrDefault();
            var invoiceStatement = new InvoiceStatement(invoice).Calculate();

            var summaryReportEn = invoiceStatement
                .Report(template => Reports.EnglishUs(template).Summary());

            var summaryReportPt = invoiceStatement
                .Report(template => Reports.CustomCulture(template, "pt-BR").Summary());

            Assert.Equal(expectedOutput, summaryReportEn);
            Assert.Equal(expectedOutputPt, summaryReportPt);
        }
    }
}
