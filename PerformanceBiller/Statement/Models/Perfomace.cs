using System;

namespace PerformanceBiller.Statement.Models
{
    public abstract class Perfomance
    {
        public int Audience { get; private set; }
        public Play Play { get; private set; }
        public decimal Amount { get; private set; }
        public decimal VolumeCredits { get; private set; }
        protected abstract decimal InitialAmount { get; }
        protected abstract int MinimiumAudience { get; }

        protected Perfomance(Builders.PerfomaceBuilder builder)
        {
            Audience = builder.Audience;
            Play = builder.Play;
            Amount = InitialAmount;
        }

        public void CalculateAmount()
        {
            if (Audience > MinimiumAudience)
                Amount += CalculateGreaterThanAudience();

            Amount += FinalCalculate();
        }

        public void CalculateVolumeCredits()
        {
            VolumeCredits = Math.Max(Convert.ToInt32(Audience) - 30, 0);
            VolumeCredits += CalculateExtraVolumeCredits();
        }

        protected abstract decimal CalculateGreaterThanAudience();
        protected virtual int CalculateExtraVolumeCredits() => 0;
        protected virtual decimal FinalCalculate() => 0;
    }
}
