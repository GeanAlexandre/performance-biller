using PerformanceBiller.Management.PlaysHandler;
using System;
using System.Globalization;
using System.Linq;

namespace PerformanceBiller.Management
{
    public class Statement
    {
        public Invoice Invoice { get; private set; }
        public decimal TotalAmount { get; private set; }
        public int VolumeCredits { get; private set; }
        public CultureInfo CultureInfo { get; private set; }

        private Statement(
            Invoice invoice,
            decimal totalAmount,
            int volumeCredits,
            CultureInfo cultureInfo
            )
        {
            Invoice = invoice;
            TotalAmount = totalAmount;
            VolumeCredits = volumeCredits;
            CultureInfo = cultureInfo;
        }

        public static Statement EnUs(Invoice invoice) => new Statement(invoice, 0, 0, new CultureInfo("en-Us"));

        public Statement Calculate(PlayHandlerFactory playHandlerFactory)
        {
            Invoice.Performances = Invoice
                .Performances
                .Select(perfomace =>
                {
                    var playHandler = playHandlerFactory.Create(perfomace.Play);

                    perfomace.AcummulateAmount(playHandler.CalculateAmount(perfomace));
                    perfomace.AcummulateVolumeCredits(playHandler.CalculateVolumeCredits(perfomace));

                    return perfomace;
                });

            TotalAmount = Invoice.Performances.Sum(p => p.Amount);
            VolumeCredits = (int)Invoice.Performances.Sum(p => p.VolumeCredits);

            return this;
        }

        public string Report(Func<Statement, string> formatter) => formatter(this);
    }
}
