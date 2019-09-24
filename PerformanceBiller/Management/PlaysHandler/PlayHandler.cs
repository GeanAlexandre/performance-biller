using System;

namespace PerformanceBiller.Management.PlaysHandler
{
    public abstract class PlayHandler
    {
        private decimal _amountAccumulate { get; set; }

        protected abstract decimal Amount { get; }
        protected abstract decimal AudienceComparator { get; }

        protected PlayHandler()
        {
            _amountAccumulate = Amount;
        }

        public decimal CalculateAmount(Perfomance perfomance)
        {
            if (perfomance.Audience > AudienceComparator)
                _amountAccumulate += CalculateGreaterThanAudience(perfomance);

            _amountAccumulate += FinalCalculate(perfomance);

            return _amountAccumulate;
        }

        public int CalculateVolumeCredits(Perfomance perfomance)
        {
            var volumeCredits = Math.Max(Convert.ToInt32(perfomance.Audience) - 30, 0);
            volumeCredits += CalculateExtraVolumeCredits(perfomance);
            return volumeCredits;
        }

        protected abstract decimal CalculateGreaterThanAudience(Perfomance perfomance);
        protected virtual int CalculateExtraVolumeCredits(Perfomance perfomance) => 0;
        protected virtual decimal FinalCalculate(Perfomance perfomance) => 0;
    }
}
