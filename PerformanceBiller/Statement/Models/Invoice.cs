using System.Collections.Generic;

namespace PerformanceBiller.Statement.Models
{
    public class Invoice
    {
        public string Customer { get; private set; }
        public IList<Perfomance> Performances { get; private set; }

        public Invoice(string customer)
        {
            Customer = customer;
            Performances = new List<Perfomance>();
        }

        public Invoice AddPerfomance(Perfomance perfomance)
        {
            Performances.Add(perfomance);

            return this;
        }
    }
}
