using PerformanceBiller.Statement.Models;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PerformanceBiller.Statement.Reports
{
    public class Reports
    {
        private readonly InvoiceStatement _invoiceStatement;
        private readonly StringBuilder _builder;
        private readonly CultureInfo _cultureInfo;

        private Reports(InvoiceStatement invoiceStatement, CultureInfo cultureInfo)
        {
            _invoiceStatement = invoiceStatement;
            _cultureInfo = cultureInfo;
            _builder = new StringBuilder();
        }

        public static Reports EnglishUs(InvoiceStatement invoiceStatement) => new Reports(invoiceStatement, new CultureInfo("en-US"));
        public static Reports CustomCulture(InvoiceStatement invoiceStatement, string cultureInfo) => new Reports(invoiceStatement, new CultureInfo(cultureInfo));

        public string Summary()
        {
            _builder.AppendLine($"Statement for {_invoiceStatement.Invoice.Customer}");

            _invoiceStatement.Invoice.Performances.ToList().ForEach(p =>
            {
                _builder.AppendLine($" {p.Play.Name}: {(p.Amount / 100).ToString("C", _cultureInfo)} ({p.Audience} seats)");
            });

            _builder.AppendLine($"Amount owed is {(_invoiceStatement.TotalAmount / 100).ToString("C", _cultureInfo)}");
            _builder.AppendLine($"You earned {_invoiceStatement.TotalVolumeCredits} credits");
            return _builder.ToString();
        }
    }
}
