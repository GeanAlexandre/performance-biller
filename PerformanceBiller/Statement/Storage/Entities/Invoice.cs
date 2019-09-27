using System.Collections.Generic;

namespace PerformanceBiller.Statement.Storage.Entities
{
    public class InvoiceEntity
    {
        public string Customer { get; set; }
        public IEnumerable<PerfomanceEntity> Performances { get; set; }
    }
}
