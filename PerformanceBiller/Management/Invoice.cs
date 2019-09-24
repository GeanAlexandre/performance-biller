using System.Collections.Generic;

namespace PerformanceBiller.Management
{
    public class Invoice
    {
        public string Customer { get; set; }
        public IEnumerable<Perfomance> Performances { get; set; }
    }
}
