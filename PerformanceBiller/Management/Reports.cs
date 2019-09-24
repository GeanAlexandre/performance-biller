using System.Linq;
using System.Text;

namespace PerformanceBiller.Management
{
    public class Reports
    {
        private readonly Statement _statement;
        private readonly StringBuilder _builder;

        private Reports(Statement statement)
        {
            _statement = statement;
            _builder = new StringBuilder();
        }

        public static Reports For(Statement statement) => new Reports(statement);

        public string Summary()
        {
            _builder.AppendLine($"Statement for {_statement.Invoice.Customer}");

            _statement.Invoice.Performances.ToList().ForEach(p =>
            {
                _builder.AppendLine($" {p.Play.Name}: {(p.Amount / 100).ToString("C", _statement.CultureInfo)} ({p.Audience} seats)");
            });

            _builder.AppendLine($"Amount owed is {(_statement.TotalAmount / 100).ToString("C", _statement.CultureInfo)}");
            _builder.AppendLine($"You earned {_statement.VolumeCredits} credits");
            return _builder.ToString();
        }
    }
}
