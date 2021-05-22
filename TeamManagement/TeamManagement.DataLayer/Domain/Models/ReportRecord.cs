using System;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class ReportRecord : IIdentificated
    {
        public Guid Id { get; set; }
        public string RecordName { get; set; }
        public string Value { get; set; }
        public Report Report { get; set; }
        public Guid ReportId { get; set; }
    }
}
