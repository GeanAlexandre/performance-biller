namespace PerformanceBiller.Statement.Models
{
    class Tragedy : Perfomance
    {
        protected override decimal InitialAmount => 40000;

        protected override int MinimiumAudience => 30;

        public Tragedy(Builders.PerfomaceBuilder builder) : base(builder) { }

        protected override decimal CalculateGreaterThanAudience()
            => 1000 * (Audience - 30);
    }
}
