using System;

namespace PerformanceBiller.Statement.Models
{
    public class Comedy : Perfomance
    {
        protected override int MinimiumAudience => 20;
        protected override decimal InitialAmount => 30000;

        public Comedy(Builders.PerfomaceBuilder builder) : base(builder) { }

        protected override decimal CalculateGreaterThanAudience()
            => 10000 + 500 * (Audience - 20);

        protected override decimal FinalCalculate()
            => 300 * Convert.ToInt32(Audience);

        protected override int CalculateExtraVolumeCredits()
            => Audience / 5;
    }
}
