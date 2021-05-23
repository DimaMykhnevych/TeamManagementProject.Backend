using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamManagement.DataLayer.Domain.Interdaces;

namespace TeamManagement.DataLayer.Domain.Models
{
    public class Report : IIdentificated
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime DateOfPublishsing { get; set; }
        public string AdditionalComment { get; set; }

        public string PublisherId { get; set; }
        public AppUser Publisher { get; set; }
        public List<ReportRecord> ReportRecords { get; set; }
    }
}
