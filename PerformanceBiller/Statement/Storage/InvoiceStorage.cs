using System.Collections.Generic;
using System.Linq;
using PerformanceBiller.Helpers;
using PerformanceBiller.Statement.Models;
using PerformanceBiller.Statement.Storage.Entities;

namespace PerformanceBiller.Statement.Storage
{
    public class InvoiceStorage
    {
        private readonly JsonFileReader _json;
        private readonly Models.Builders.PerfomaceBuilder _perfomaceBuilder;

        public InvoiceStorage()
        {
            _json = JsonFileReader.From(@"..\\..\\..\");
            _perfomaceBuilder = new Models.Builders.PerfomaceBuilder();
        }

        public virtual IReadOnlyList<Invoice> GetAll()
        {
            var plays = _json.Read<Dictionary<string, PlayEntity>>("plays");
            var invoices = _json.Read<InvoiceEntity[]>("invoices");

            return invoices
                .Select(invoice => invoice.Performances
                .Aggregate(new Invoice(invoice.Customer),
                    (accumulateInvoice, perfomance) =>
                    accumulateInvoice
                    .AddPerfomance(_perfomaceBuilder
                    .WithPerfomanceEntity(perfomance)
                    .WithPlayEntity(plays.GetValueOrDefault(perfomance.PlayId))
                    .Build())))
                .ToList();
        }
    }
}
