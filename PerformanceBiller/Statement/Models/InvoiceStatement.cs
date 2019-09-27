using System;
using System.Linq;

namespace PerformanceBiller.Statement.Models
{
    public class InvoiceStatement
    {
        public Invoice Invoice { get; private set; }
        public decimal TotalAmount => Invoice.Performances.Sum(p => p.Amount);
        public int TotalVolumeCredits => (int)Invoice.Performances.Sum(p => p.VolumeCredits);

        public InvoiceStatement(Invoice invoice)
        {
            Invoice = invoice;
        }

        public InvoiceStatement Calculate()
        {
            Invoice
                .Performances
                .ToList()
                .ForEach(perfomace =>
                {
                    perfomace.CalculateAmount();
                    perfomace.CalculateVolumeCredits();
                });

            return this;
        }

        public string Report(Func<InvoiceStatement, string> formatter) => formatter(this);
    }
}
