namespace PerformanceBiller.Management
{
    public class Perfomance
    {
        public decimal Amount { get; private set; }
        public decimal VolumeCredits { get; private set; }
        public Play Play { get; set; }
        public string PlayId { get; set; }
        public int Audience { get; set; }

        public void AcummulateAmount(decimal amount)
        {
            Amount += amount;
        }

        public void AcummulateVolumeCredits(decimal volumeCredits)
        {
            VolumeCredits += volumeCredits;
        }
    }
}
