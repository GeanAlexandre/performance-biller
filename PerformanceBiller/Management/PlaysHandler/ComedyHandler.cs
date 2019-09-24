using System;

namespace PerformanceBiller.Management.PlaysHandler
{
    public class ComedyHandler : PlayHandler
    {
        protected override decimal Amount => 30000;

        protected override decimal AudienceComparator => 20;

        protected override decimal CalculateGreaterThanAudience(Perfomance perfomance)
            => 10000 + 500 * (perfomance.Audience - 20);

        protected override decimal FinalCalculate(Perfomance perfomance)
            => 300 * Convert.ToInt32(perfomance.Audience);

        protected override int CalculateExtraVolumeCredits(Perfomance perfomance) 
            => perfomance.Audience / 5;
    }
}
