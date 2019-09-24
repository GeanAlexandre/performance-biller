namespace PerformanceBiller.Management.PlaysHandler
{
    public class TragedyHandler : PlayHandler
    {
        protected override decimal Amount => 40000;
        protected override decimal AudienceComparator => 30;
        protected override decimal CalculateGreaterThanAudience(Perfomance perfomance) 
            => 1000 * (perfomance.Audience - 30);
    }
}
